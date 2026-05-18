namespace CodingNow.Lecture.Oop09;

internal class Vehicle
{
    public virtual void Move() => Console.WriteLine("탈것 이동");
}

internal class Car : Vehicle
{
    public override void Move() => Console.WriteLine("자동차가 부릉~");
}

internal class Bike : Vehicle
{
    public override void Move() => Console.WriteLine("자전거가 따르릉~");
}
