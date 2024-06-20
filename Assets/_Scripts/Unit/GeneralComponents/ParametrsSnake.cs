public class ParametrsSnake {
    public float DistanceBeetwenSection { get; private set; }
    public Section Head { get; private set; }
    public float Speed { get; private set; }

    public ParametrsSnake(SnakeConfig snakeConfig, Section head) {
        DistanceBeetwenSection = snakeConfig.DistanceBeetwen;
        Speed = snakeConfig.Speed;
        Head = head;
    }

    public void SetNewSpeed(float speed) {
        if (speed > 0)
            Speed = speed;
    }
}