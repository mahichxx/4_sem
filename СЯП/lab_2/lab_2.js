var ProductCategory;
(function (ProductCategory) {
    ProductCategory["Electronics"] = "electronics";
    ProductCategory["Clothing"] = "clothing";
    ProductCategory["Book"] = "book";
    ProductCategory["Food"] = "food";
})(ProductCategory || (ProductCategory = {}));
class Product {
    constructor(id, name, price, category, description) {
        this.id = id;
        this.price = price;
        this.name = name;
        this.description = description;
        this.category = category;
    }
    getInfo() {
        return `Product:${this.name}, ID:${this.id}, Category:${this.category}, Price:${this.price}, Discription:${this.description || 'No description'}`;
    }
}
let myProduct = new Product(16, "Chips", 466, ProductCategory.Food);
console.log(myProduct.getInfo());
class Catalog {
    constructor() {
        this.products = [];
    }
    addProduct(product) {
        this.products.push(product);
    }
    removeProduct(id) {
        this.products = this.products.filter(p => p.id != id);
    }
    getProductById(id) {
        return this.products.find(p => p.id == id);
    }
    getAllProducts() {
        return [...this.products];
    }
    getProductsByCategory(category) {
        return this.products.filter(p => p.category == category);
    }
}
class Order {
    constructor(id, customer, products, totalPrice = 0) {
        this.id = id;
        this.customer = customer;
        this.products = products;
        this.totalPrice = totalPrice;
    }
    calculateTotalPrice() {
        this.totalPrice = this.products.reduce((sum, product) => sum + product.price, 0);
    }
    getOrderInfo() {
        const productNames = this.products.map(p => p.name).join(',');
        return `order ID:${this.id}, products:${productNames}, total:${this.totalPrice}`;
    }
}
class Customer {
    constructor(id, name, email) {
        this.id = id;
        this.name = name;
        this.email = email;
    }
    getCustomerInfo() {
        return `Customer:${this.name}, ID:${this.id}, Email:${this.email}`;
    }
}
class OrderManager {
    constructor() {
        this.orders = [];
    }
    createOrder(customer, products) {
        const id = this.orders.length + 1;
        const order = new Order(id, customer, products);
        order.calculateTotalPrice();
        this.orders.push(order);
        return order;
    }
    getOrderById(id) {
        return this.orders.find(o => o.id === id);
    }
    getAllOrders() {
        return [...this.orders];
    }
    getRemoveOrder(id) {
        return this.orders = this.orders.filter(p => p.id != id);
    }
    getOrdersByCustomer(customerId) {
        return this.orders.filter(o => o.customer.id === customerId);
    }
}
const customer1 = new Customer(1, "Алексей", "alex@example.com");
const customer2 = new Customer(2, "Мария", "maria@example.com");
const laptop = new Product(1, "Ноутбук", 85000, ProductCategory.Electronics);
const chips = new Product(3, "Чипсы", 120, ProductCategory.Food);
const myCatalog = new Catalog();
myCatalog.addProduct(myProduct);
console.log(myCatalog);
const manager = new OrderManager();
const order1 = manager.createOrder(customer1, [laptop, chips]);
const order2 = manager.createOrder(customer2, [chips]);
console.log(manager.getAllOrders());
const remaining = manager.getRemoveOrder(1);
console.log(manager.getAllOrders());
export {};
