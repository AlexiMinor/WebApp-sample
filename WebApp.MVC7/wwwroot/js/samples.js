console.log('Hello from file');

let smth = true;

console.log(smth, typeof (smth));

smth = 15;
console.log(smth, typeof (smth));

smth = "Hello world";
console.log(smth, typeof (smth));

smth = false;
console.log(smth, typeof (smth));

smth = { A: 15, BN: 487 };
console.log(smth, typeof (smth));

smth = null;
console.log(smth, typeof (smth));
smth = undefined;
console.log(smth, typeof (smth));
smth = NaN;
console.log(smth, typeof (smth));

let array = ["Hello", "Salut", "Heil", 12];

array.push(123);

for (let i = 0; i < array.length; i++) {
    console.log(array[i]);
}

console.log(array);

console.log(array.pop());
console.log(array);

console.log(array.shift());
console.log(array);

if (6 > 5) {
    console.log('bool',true);
}

if (6) {
    console.log(6, 'number');
}

if (0) {
    console.log(0, 'number');
}

if (-1) {
    console.log(-1, 'number');
}

let str = 'str';
if (str) {
    console.log(str, 'string');
}

str = 'false';
if (str) {
    console.log(str, 'string');
}


let obj = { A:"" }
if (str) {
    console.log(obj, 'object');
}

if (null) {
    console.log(null, 'null');
}

if (undefined) {
    console.log(undefined, 'undefined');
}

//math operations
console.log(5 % 2);//1

console.log('Hello ' + 'world');
console.log('1' + 2);//12
console.log(2 + 1);//3
console.log(2 + '1');//21
console.log(2+2 + '1');//41
console.log('1' + 2 + 2);//122

console.log(6-'15');//-9
console.log('6'-15);//-9
console.log('116'-'6');
console.log('6' / '2');//3

//unary +
let value = 1;
console.log(+value);

value = -1;
console.log(+value);

console.log(+true);
console.log(+"");

console.log('2' + '3');//23
console.log(+'2' + '3');
console.log(Number('2') + Number('3'));
console.log(+'2' + +'3');

console.log(2 + 2 * 2);

let n = printSmth();

let x = sampleOfError();
console.log(x);

printHelloWorld();
let doSmth = function () {
    console.log('Do');
}
doSmth();

function printHelloWorld() {
    console.log('Hello world');
}

function printSmth(smth, smthElse="qwe") {
    console.log(smth)
    console.log(smthElse)
    return 1;
}

function sampleOfError() {
    return (
        "Hello" + "World" + "123" + 11111111111111111111111111111111111111111111111);
}

function getData(x) {
    if (x) { return 12; }
    else {return 'Hello' }
}