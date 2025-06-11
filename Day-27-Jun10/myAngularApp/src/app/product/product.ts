import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-product',
  imports: [CommonModule],
  templateUrl: './product.html',
  styleUrls: ['./product.css']
})
export class Product {
  cartCount: number = 0;

products = [
  {name: "T shirts", image: "https://storage.googleapis.com/kagglesdsdata/datasets/1235849/2061927/data/Apparel/Boys/Images/images_with_product_ids/10054.jpg?X-Goog-Algorithm=GOOG4-RSA-SHA256&X-Goog-Credential=gcp-kaggle-com%40kaggle-161607.iam.gserviceaccount.com%2F20250610%2Fauto%2Fstorage%2Fgoog4_request&X-Goog-Date=20250610T092706Z&X-Goog-Expires=259200&X-Goog-SignedHeaders=host&X-Goog-Signature=f468dc600ea8c56a9d64c84a92a022773efde3f9bfc3466a28b6d96f7de8faeba4b94e37866ad205db380affc2828f529fb3912c51483069be1aa6c07d08411589c9671c4f91a6695ce77d326f77bc0fe56f1a1ead4d7ee4f2e8f7ca8d154a6327893f52edb9908cb7e5569dc2225fa5b8735f7aabaa671f10b4d2a8af2d7615eab98d4f9d444c2f47ed02ea034a5617ef36827f540d3ed46e0ad8627d521f4fe0c11c9476b13d89a0f667887ae4d2e4b5c882ac3510f50afb70502ea61bec7658f8caa91edb50ee3379a846535b701254e40649684c97e7124e8c73a59b7a9ecc944022405a8e7129193c62d5821ca071d23f2f6a97ef9b26ee14a47c3d51c0"},
  {name: "Shirts", image: "https://duders.in/cdn/shop/files/MenShirts_4_800x1000.png?v=1702725005"},
  {name: "Hoodie", image: "https://cdn-images.farfetch-contents.com/27/48/39/46/27483946_57262506_2048.jpg"}
]

  addToCart(){
    this.cartCount++;
  }
}
