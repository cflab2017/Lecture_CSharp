// var: 컴파일러가 우변을 보고 타입 추론
var name = "Alice";   // string
var age = 30;         // int

// const: 컴파일 타임 상수 (이후 변경 불가)
const double Pi = 3.14159;
const string Greet = "안녕!";

Console.WriteLine($"{name}, {age}세, {Greet} (Pi={Pi})");
// Pi = 3.0;   // 컴파일 에러
