

using Linq.Classes;

int[] arr = { 1, 2, 3, 4, 4, 5, 6, 7, 8 };

// var smallNumbers = from num in arr where num < 5 select num;

var smallNumbers = arr.Where((num, idx) => idx >= 5);

foreach (int smallNum in smallNumbers)
    Console.Write($"{smallNum} ");

List<Product> GetProductList()
{
    List<Product> productsList = new List<Product>();
    productsList.Add(new Product { ProductName = "Product 1", UnitsInStock = 3, UnitPrice = 3.00m });
    productsList.Add(new Product { ProductName = "Product 2", UnitsInStock = 0, UnitPrice = 4.00m });
    productsList.Add(new Product { ProductName = "Product 3", UnitsInStock = 5, UnitPrice = 6.00m });
    productsList.Add(new Product { ProductName = "Product 4", UnitsInStock = 1, UnitPrice = 1.00m });
    productsList.Add(new Product { ProductName = "Product 5", UnitsInStock = 0, UnitPrice = 2.00m });
    productsList.Add(new Product { ProductName = "Product 6", UnitsInStock = 2, UnitPrice = 1.00m });
    productsList.Add(new Product { ProductName = "Product 7", UnitsInStock = 0, UnitPrice = 7.00m });
    productsList.Add(new Product { ProductName = "Product 8", UnitsInStock = 3, UnitPrice = 8.00m });


    return productsList;
}

void Print(IEnumerable<Product> list){
    foreach (Product prod in list)
    {
        Console.WriteLine(prod);
    }
}


var product_1 = GetProductList();

var result = product_1.Where(prod => prod.UnitsInStock > 0 && prod.UnitPrice < 3.00m);

Print(result);

var distinctResult = arr.Distinct();

foreach (int n in distinctResult)
{
    Console.Write(n);
    Console.Write(" ");
}