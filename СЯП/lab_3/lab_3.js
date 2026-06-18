var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g = Object.create((typeof Iterator === "function" ? Iterator : Object).prototype);
    return g.next = verb(0), g["throw"] = verb(1), g["return"] = verb(2), typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var _this = this;
// Задание 1
function delay(ms) {
    return new Promise(function (resolve) { return setTimeout(resolve, ms); });
}
function checkStock(item) {
    return delay(1000).then(function () {
        var inStock = Math.random() > 0.2;
        if (!inStock) {
            return Promise.reject(new Error("\u0422\u043E\u0432\u0430\u0440\u0430 \"".concat(item, "\" \u043D\u0435\u0442 \u043D\u0430 \u0441\u043A\u043B\u0430\u0434\u0435")));
        }
        console.log("\u0422\u043E\u0432\u0430\u0440 \"".concat(item, "\" \u0435\u0441\u0442\u044C \u043D\u0430 \u0441\u043A\u043B\u0430\u0434\u0435"));
        return { item: item, price: 450 };
    });
}
function processPayment(order) {
    return delay(2000).then(function () {
        var balance = Math.floor(Math.random() * 1000);
        console.log("\u0411\u0430\u043B\u0430\u043D\u0441: ".concat(balance, " \u0440\u0443\u0431."));
        if (balance < 500) {
            return Promise.reject(new Error("Недостаточно средств"));
        }
        console.log("\u041E\u043F\u043B\u0430\u0442\u0430 \u043F\u0440\u043E\u0448\u043B\u0430 \u0443\u0441\u043F\u0435\u0448\u043D\u043E");
        return __assign(__assign({}, order), { paid: true });
    });
}
function deliverOrder(order) {
    return delay(1500).then(function () {
        console.log("\u0417\u0430\u043A\u0430\u0437 \"".concat(order.item, "\" \u0434\u043E\u0441\u0442\u0430\u0432\u043B\u0435\u043D"));
        return order;
    });
}
function makeOrder(item) {
    checkStock(item)
        .then(processPayment)
        .then(deliverOrder)
        .then(function (order) { return console.log("Итог заказа:", order); })
        .catch(function (err) { return console.error("Ошибка:", err.message); })
        .finally(function () { return console.log("Спасибо за заказ!"); });
}
// Задание 2
function fetchFast() {
    return delay(500).then(function () { return "fetchFast"; });
}
function fetchSlow() {
    return delay(2000).then(function () { return "fetchSlow"; });
}
function raceExample() {
    Promise.race([fetchFast(), fetchSlow()])
        .then(function (result) { return console.log("Победитель гонки:", result); })
        .catch(function (err) { return console.error("Ошибка:", err); });
}
// Задание 3
function createPromisesForAllSettled() {
    return [
        delay(300).then(function () { return "Успех 1"; }),
        delay(500).then(function () { return "Успех 2"; }),
        delay(700).then(function () { return "Успех 3"; }),
        delay(400).then(function () { throw new Error("Ошибка 4"); }),
        delay(600).then(function () { throw new Error("Ошибка 5"); })
    ];
}
function allSettledExample() {
    Promise.allSettled(createPromisesForAllSettled())
        .then(function (results) {
        console.log("Результаты allSettled:");
        results.forEach(function (res, i) {
            if (res.status === "fulfilled") {
                console.log("\u041F\u0440\u043E\u043C\u0438\u0441 ".concat(i + 1, ":"), res.value);
            }
        });
    });
}
// Задание 4
function microtaskExample() {
    console.log("Начало");
    setTimeout(function () { return console.log("Таймаут"); }, 0);
    Promise.resolve()
        .then(function () { return console.log("Промис 1"); })
        .then(function () { return console.log("Промис 2"); });
    console.log("Конец");
}
// Задание 5
function getData() {
    return __awaiter(this, void 0, void 0, function () {
        var response, data, err_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 3, , 4]);
                    return [4 /*yield*/, fetch("https://api.example.com/data")];
                case 1:
                    response = _a.sent();
                    if (!response.ok) {
                        throw new Error("HTTP \u043E\u0448\u0438\u0431\u043A\u0430: ".concat(response.status));
                    }
                    return [4 /*yield*/, response.json()];
                case 2:
                    data = _a.sent();
                    console.log("Данные:", data);
                    return [3 /*break*/, 4];
                case 3:
                    err_1 = _a.sent();
                    console.error("Ошибка:", err_1);
                    return [3 /*break*/, 4];
                case 4: return [2 /*return*/];
            }
        });
    });
}
// Задание 6
function limitRequests(urls, limit) {
    return __awaiter(this, void 0, void 0, function () {
        var results, current, active;
        return __generator(this, function (_a) {
            results = new Array(urls.length);
            current = 0;
            active = 0;
            return [2 /*return*/, new Promise(function (resolve) {
                    function run() {
                        if (current >= urls.length && active === 0) {
                            resolve(results);
                            return;
                        }
                        var _loop_1 = function () {
                            var index = current++;
                            var url = urls[index];
                            active++;
                            fakeFetch(url)
                                .then(function (res) { return results[index] = res; })
                                .catch(function (err) { return results[index] = err; })
                                .finally(function () {
                                active--;
                                run();
                            });
                        };
                        while (active < limit && current < urls.length) {
                            _loop_1();
                        }
                    }
                    run();
                })];
        });
    });
}
function fakeFetch(url) {
    var time = 500 + Math.random() * 1500;
    return delay(time).then(function () { return "\u0417\u0430\u0433\u0440\u0443\u0436\u0435\u043D\u043E ".concat(url, " \u0437\u0430 ").concat(Math.round(time), "\u043C\u0441"); });
}
makeOrder("Пицца");
raceExample();
allSettledExample();
microtaskExample();
getData();
(function () { return __awaiter(_this, void 0, void 0, function () {
    var urls, res;
    return __generator(this, function (_a) {
        switch (_a.label) {
            case 0:
                urls = Array.from({ length: 10 }, function (_, i) { return "img_".concat(i + 1, ".png"); });
                return [4 /*yield*/, limitRequests(urls, 3)];
            case 1:
                res = _a.sent();
                console.log("limitRequests:", res);
                return [2 /*return*/];
        }
    });
}); })();
