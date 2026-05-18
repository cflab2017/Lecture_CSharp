# 과제 - 09. 다형성

## 문제 1 — `Vehicle`/`Car`/`Bike` (`Move()` 다형성)
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `virtual` / `override`, 컬렉션에 자식 객체 담기

### 요구사항
- 부모 `Vehicle` 클래스에 `virtual void Move()` 가 있다(기본 메시지 출력).
- 자식 `Car`, `Bike` 가 각각 `Move()` 를 override 해 다른 메시지를 출력한다.
- `Program.cs` 에서 `List<Vehicle>` 에 객체 3~4 개를 담고 순회하며 `Move()` 호출.

### 예상 출력
```
탈것 이동
자동차가 부릉~
자전거가 따르릉~
자동차가 부릉~
```

### 힌트
- `List<Vehicle>` 에 `new Car()`, `new Bike()` 를 그대로 담으면 된다(업캐스팅).

## 문제 2 — `abstract Animal` + 자식들 `Speak()`
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `abstract` 클래스 / `abstract` 메서드, `is` 패턴

### 요구사항
- `abstract class Animal` 에 `abstract void Speak()` 를 둔다.
- 자식 `Dog`, `Cat`, `Cow` 각각 `Speak()` override.
- `Dog` 에만 추가 메서드 `Bark()` 를 둔다.
- `Animal[] zoo = [...]` 를 순회하면서 `Speak()` 호출, `is Dog dog` 일 때 `dog.Bark()` 도 호출.

### 예상 출력
```
멍멍!
왕!왕!
야옹~
음매~
```

### 힌트
- `abstract` 메서드는 본문이 없고 세미콜론으로 끝난다.
- `var animal = new Animal();` 는 컴파일 에러 (추상 클래스는 직접 못 만든다).

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
