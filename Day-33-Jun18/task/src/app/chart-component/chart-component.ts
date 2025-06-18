import { Component } from '@angular/core';
import { UserModel } from '../models/userModel';
import { ChartType, GoogleChartsModule } from 'angular-google-charts';
import { UserService } from '../services/userService';

@Component({
  selector: 'app-chart-component',
  imports: [GoogleChartsModule],
  templateUrl: './chart-component.html',
  styleUrl: './chart-component.css'
})
export class ChartComponent {
  users: UserModel[] = [];
  genderChartData: any = [];
  roleChartData: any = [];
  stateGeoChartData: any = []


  pieType = ChartType.PieChart;
  barType = ChartType.BarChart;
  geoType = ChartType.GeoChart;

  columns = ['Gender', 'Count'];
  geoColumns = ['State', 'Users']; 

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getAllUsers().subscribe({
      next: (data: any) => {
        this.users = data.users as UserModel[];
        console.log(this.users)
        this.updateChartModule();
        this.updateRoleChart();
        this.updateStateGeoChart();
      }
    })
  }

  updateChartModule() {
    const maleCount = this.users.filter(u => u.gender == 'male').length;
    const femaleCount = this.users.filter(u => u.gender == 'female').length;

    console.log(maleCount, femaleCount)
    this.genderChartData = [
      ['Male', maleCount],
      ['Female', femaleCount]
    ]
  }

  updateRoleChart() {
    const roleCountMap = new Map<string, number>();

    this.users.forEach(user => {
      roleCountMap.set(user.role, (roleCountMap.get(user.role) || 0) + 1)
    }
    )

    this.roleChartData = Array.from(roleCountMap.entries());
  }

  updateStateGeoChart() {
    const stateCountMap = new Map<string, number>();
    this.users.forEach(user => {
      const state = user.address.state;
      if (state) {
        stateCountMap.set(state, (stateCountMap.get(state) || 0) + 1);
      }
    });

    this.stateGeoChartData = Array.from(stateCountMap.entries());
  }


}
