namespace StreamingApiDemo.Models;

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }

	// Parameterless ctor required by EF
	public Product() { }

	public Product(int id, string name, decimal price)
	{
		Id = id;
		Name = name;
		Price = price;
	}
}