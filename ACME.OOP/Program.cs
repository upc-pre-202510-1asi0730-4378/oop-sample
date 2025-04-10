// See https://aka.ms/new-console-template for more information

using ACME.OOP.Procurement.Domain.Model.Aggregates;
using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.Aggregates;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

// Create a new supplier
var supplierAddress = new Address("Supplier Street", "123", "Supplier City", "12345", "Supplier Code", "United States");
var supplier = new Supplier("SUP001", "Microsoft, inc.", supplierAddress);

// Create an order
var purchaseOrder = new PurchaseOrder("PO001", supplier.SupplierId, DateTime.Now, "USD");
purchaseOrder.AddItem(ProductId.New(),10,25.99m);
purchaseOrder.AddItem(ProductId.New(),5,15.99m);

// Show the order details
Console.WriteLine($"Purchase Order ID: {purchaseOrder.OrderNumber} created for Supplier ID: {purchaseOrder.SupplierId.Identifier} in {purchaseOrder.Currency}");

foreach (var item in purchaseOrder.Items)
{
    var purchaseOrderItemSubtotal = item.CalculateSubtotal();
    Console.WriteLine($"Item Subtotal: {purchaseOrderItemSubtotal.Amount} {purchaseOrderItemSubtotal.Currency}" );
}

var purchaseOrderTotal = purchaseOrder.CalculateTotal();
Console.WriteLine($"Purchase Order Total: {purchaseOrderTotal.Amount} {purchaseOrderTotal.Currency}" );