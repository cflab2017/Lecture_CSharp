using CodingNow.Lecture.Oop10;

ILogger lg = new ConsoleLogger();
lg.Log("정상 메시지");
lg.Warn("이건 경고");   // ConsoleLogger 가 구현 안 했으니 인터페이스 기본 구현이 호출됨

// new ConsoleLogger().Warn("...");   // 기본 구현은 인터페이스 타입으로만 호출 가능
