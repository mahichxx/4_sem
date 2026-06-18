
const input = document.querySelector('#itemInput');
const input2 = document.querySelector('#syncInput');
const btn = document.querySelector('#addBtn');
const list = document.querySelector('#myList');
const colorBtn = document.querySelector('#colorBtn');
const colorBox = document.querySelector('#colorBox');

btn.onclick = function (){
    const text = input.value;

    if (text !== ""){
        const li = document.createElement('li');
        li.textContent = text;
        list.appendChild(li);
        input.value = "";
    }
};

list.onclick = function(event){
    if(event.target.tagName === 'LI') {
        event.target.remove();
    }

};

colorBtn.onclick = function (){
    colorBox.style.backgroundColor = 'red';
}

input.onclick = function(){
    input.value = input2.value;
}

const user = {
    name: "Ivan",
    age: 11,
    city: "Bologin",
    hobby: "proga"
};

const { name, age, ...otherInfo } = user;

console.log(name);
console.log(otherInfo);

const arr1 = [1, 2];
const arr2 = [3, 4];

const arr3 = [...arr1, ...arr2];
const student = [
    {name: "aaa", grade: 5 },
    {name: "bbb", grade: 4 },
    {name: "ccc", grade: 3 }
];

const names = student.map(st => st.name);
console.log(names);

const totalSum = student.reduce((sum, st) => sum + st.grade, 0);
const average = totalSum / student.length;

console.log(average);

const user1 ={
    name: "Lala",
    sayHi(){
        console.log('Hello, my name is ${this.name}');
    }
};

const tack1 = user1.sayHi.bind(user1);
console.log(tack1, 1000);



