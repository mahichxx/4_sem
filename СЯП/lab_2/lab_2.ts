export {};

enum ProductCategory{
    Electronics = "electronics",
    Clothing = "clothing",
    Book = "book",
    Food = "food",
}

class Product {
    id: number;
    name: string;
    price: number;
    description?: string;
    category: ProductCategory;

    constructor (id: number, name:string, price: number, category: ProductCategory, description?: string){
        this.id = id;
        this.price = price;
        this.name = name;
        this.description = description;
        this.category = category;
    }
    getInfo(): string{
    return `Product:${this.name}, ID:${this.id}, Category:${this.category}, Price:${this.price}, Discription:${this.description || 'No description'}`
}
}

let myProduct = new Product(
    16,
    "Chips",
    466,
    ProductCategory.Food
);

console.log(myProduct.getInfo())

type PartialProduct = Partial<Product>;
type NewProduct = Omit<Product, 'id'>;
type OrderSummary = Pick<Order<Product>, 'id' | 'totalPrice'>;

const test: OrderSummary = {
    id: 101,
    totalPrice: 4500
}
//test.customer = customer1;

class Catalog {
     products: Product[] = [];
    
    addProduct(product: Product): void{
        this.products.push(product)
    }

    removeProduct(id: number) : void{
        this.products = this.products.filter(p => p.id != id)
    }

    getProductById(id: number): Product | undefined {
        return this.products.find(p => p.id == id)
    }

    getAllProducts(): Product[]{
        return [...this.products];
    }

    getProductsByCategory(category: ProductCategory) : Product[]{
        return this.products.filter(p => p.category == category);
    }
}

class Order<T extends Product> {
    id: number;
    customer: Customer;
    products: T[];
    totalPrice: number;

    constructor(id: number, customer: Customer, products: T[], totalPrice: number = 0){
        this.id = id;
        this.customer = customer;
        this.products = products;
        this.totalPrice = totalPrice;
    }

    calculateTotalPrice(): void{
        this.totalPrice = this.products.reduce((sum, product) => sum + product.price , 0);
    }

    getOrderInfo(): string{
        const productNames = this.products.map(p => p.name).join(',');
        return `order ID:${this.id}, products:${productNames}, total:${this.totalPrice}`
    }
}

class Customer{
    public id: number;
    name: string;
    email: string;

    constructor(id: number, name: string, email: string){
        this.id = id;
        this.name = name;
        this.email = email;
    }

    getCustomerInfo(): string{
        return `Customer:${this.name}, ID:${this.id}, Email:${this.email}`;
    }
}

class OrderManager {
    private orders: Order<Product>[] = [];

    createOrder(customer: Customer, products: Product[]): Order<Product> {
        const id = this.orders.length + 1;
        
        const order = new Order(id, customer, products);
        order.calculateTotalPrice();
        
        this.orders.push(order);
        
        return order;
    }

    getOrderById(id: number): Order<Product> | undefined {
        return this.orders.find(o => o.id === id);
    }

    getAllOrders(): Order<Product>[] {
        return [...this.orders];
    }

    getRemoveOrder(id: number) : Order<Product>[]{
       return this.orders = this.orders.filter(p => p.id != id)
    }
    
    getOrdersByCustomer(customerId: number): Order<Product>[] {
        return this.orders.filter(o => o.customer.id === customerId);
    }
}

const customer1 = new Customer(1, "Алексей", "alex@example.com");
const customer2 = new Customer(2, "Мария",   "maria@example.com");

const laptop  = new Product(1, "Ноутбук",  85000, ProductCategory.Electronics);
const chips   = new Product(3, "Чипсы",     120, ProductCategory.Food);

const myCatalog = new Catalog();
myCatalog.addProduct(myProduct);
//console.log(myCatalog.products);

const manager = new OrderManager();

const order1 = manager.createOrder(customer1, [laptop, chips]);
const order2 = manager.createOrder(customer2, [chips]);

console.log(manager.getAllOrders());

const remaining = manager.getRemoveOrder(1);

console.log(manager.getAllOrders());