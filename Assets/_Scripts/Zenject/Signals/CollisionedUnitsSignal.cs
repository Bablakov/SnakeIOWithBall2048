public class CollisionedUnitsSignal {
    public readonly Unit RecorderCollision;
    public readonly Unit CollisionedUnit;

    public CollisionedUnitsSignal(Unit recorderCollision, Unit collisionedUnit) {
        RecorderCollision = recorderCollision;
        CollisionedUnit = collisionedUnit;
    }
}