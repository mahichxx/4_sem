type Order = {
    item: string;
    price: number;
    paid?: boolean;
};

// 1

function delay(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function checkStock(item: string): Promise<Order> {
    return delay(1000).then(() => {
        const inStock = Math.random() > 0.2;
        if (!inStock) {
            return Promise.reject(new Error(`Товара "${item}" нет на складе`));
        }
        console.log(`Товар "${item}" есть на складе`);
        return { item, price: 450 };
    });
}

function processPayment(order: Order): Promise<Order> {
    return delay(2000).then(() => {
        const balance = Math.floor(Math.random() * 1000);
        console.log(`Баланс: ${balance} руб.`);
        if (balance < 500) {
            return Promise.reject(new Error("Недостаточно средств"));
        }
        console.log(`Оплата прошла успешно`);
        return { ...order, paid: true };
    });
}

function deliverOrder(order: Order): Promise<Order> {
    return delay(1500).then(() => {
        console.log(`Заказ "${order.item}" доставлен`);
        return order;
    });
}

function makeOrder(item: string): void {
    checkStock(item)
        .then(processPayment)
        .then(deliverOrder)
        .then(order => console.log("Итог заказа:", order))
        .catch(err => console.error("Ошибка:", err.message))
        .finally(() => console.log("Спасибо за заказ!"));
}

// 2
function fetchFast(): Promise<string> {
    return delay(500).then(() => "fetchFast");
}

function fetchSlow(): Promise<string> {
    return delay(2000).then(() => "fetchSlow");
}

function raceExample(): void {
    Promise.race([fetchFast(), fetchSlow()])
        .then(result => console.log("Победитель гонки:", result))
        .catch(err => console.error("Ошибка:", err));
}

// 3
function createPromisesForAllSettled(): Promise<string>[] {
    return [
        delay(300).then(() => "Успех 1"),
        delay(500).then(() => "Успех 2"),
        delay(700).then(() => "Успех 3"),
        delay(400).then(() => { throw new Error("Ошибка 4"); }),
        delay(600).then(() => { throw new Error("Ошибка 5"); })
    ];
}

function allSettledExample(): void {
    Promise.allSettled(createPromisesForAllSettled())
        .then(results => {
            console.log("Результаты allSettled:");
            results.forEach((res, i) => {
                if (res.status === "fulfilled") {
                    console.log(`Промис ${i + 1}:`, res.value);
                }
            });
        });
}

// 4
function microtaskExample(): void {
    console.log("Начало");

    setTimeout(() => console.log("Таймаут"), 0);

    Promise.resolve()
        .then(() => console.log("Промис 1"))
        .then(() => console.log("Промис 2"));

    console.log("Конец");
}

// 5
async function getData(): Promise<void> {
    try {
        const response = await fetch("https://api.example.com/data");
        
        if (!response.ok) {
            throw new Error(`HTTP ошибка: ${response.status}`);
        }

        const data = await response.json();
        console.log("Данные:", data);
    } catch (err) {
        console.error("Ошибка:", err);
    }
}

// 6
async function limitRequests(urls: string[], limit: number): Promise<(string | Error)[]> {
    const results: (string | Error)[] = new Array(urls.length);
    let current = 0;
    let active = 0;

    return new Promise(resolve => {
        function run() {
            if (current >= urls.length && active === 0) {
                resolve(results);
                return;
            }

            while (active < limit && current < urls.length) {
                const index = current++;
                const url = urls[index];
                active++;

                fakeFetch(url)
                    .then(res => results[index] = res)
                    .catch(err => results[index] = err)
                    .finally(() => {
                        active--;
                        run();
                    });
            }
        }
        run();
    });
}

function fakeFetch(url: string): Promise<string> {
    const time = 500 + Math.random() * 1500;
    return delay(time).then(() => `Загружено ${url} за ${Math.round(time)}мс`);
}

makeOrder("Пицца");
raceExample();
allSettledExample();
microtaskExample();
getData();

(async () => {
    const urls = Array.from({ length: 10 }, (_, i) => `img_${i + 1}.png`);
    const res = await limitRequests(urls, 3);
    console.log("limitRequests:", res);
})();
