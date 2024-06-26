public class ConflictedSignal {
    public readonly CollisionHandler RecorderConflict;
    public readonly CollisionHandler ConflictedUnit;

    public ConflictedSignal(CollisionHandler recorderConflict, CollisionHandler conflictedUnit) {
        RecorderConflict = recorderConflict;
        ConflictedUnit = conflictedUnit;
    }
}