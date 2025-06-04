using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace HealthCareAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<int, Appointment> _appointmentRepository;
        private readonly IRepository<int, Patient> _patientRepository;
        private readonly IRepository<int, Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IRepository<int, Appointment> appointmentRepository,
                                IRepository<int, Patient> patientRepository,
                                IRepository<int, Doctor> doctorRepository,
                                IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;

            _patientRepository = patientRepository;

            _doctorRepository = doctorRepository;

            _mapper = mapper;
        }

        [Authorize]
        public async Task<Appointment> BookAppointment(AppointmentAddRequestDto appointment)
        {
            try
            {
                var doctor = await _doctorRepository.GetById(appointment.DoctorId);
                var patient = await _patientRepository.GetById(appointment.PatientId);

                if (appointment.AppointmentDate < DateTime.Now)
                    throw new Exception("Date not valid");


                var newAppointment = _mapper.Map<AppointmentAddRequestDto, Appointment>(appointment);
                newAppointment.Status = "Booked";
                newAppointment = await _appointmentRepository.Add(newAppointment);

                return newAppointment;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Appointment> CancelAppointment(AppointmentCancelRequestDto cancelRequest)
        {
            var appointment = await _appointmentRepository.GetById(cancelRequest.AppointmentId);
            var doctor = await _doctorRepository.GetById(cancelRequest.DoctorId) ?? throw new Exception("No Such Doctor");
            if (appointment == null || appointment.Status == "Cancelled" )
                throw new Exception("Appointment Invalid / Already Cancelled");

            if (doctor.Id != appointment.DoctorId)
                throw new Exception("Invalid Doctor");

            appointment.Status = "Cancelled";

            return await _appointmentRepository.Update(cancelRequest.AppointmentId, appointment);
        }
    }
}