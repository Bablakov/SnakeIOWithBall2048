public class ConflictedUnitsSignal {
    public readonly Unit RecorderConflict;
    public readonly Unit ConflictedUnit;

    public ConflictedUnitsSignal(Unit recorderConflict, Unit conflictedUnit) {
        RecorderConflict = recorderConflict;
        ConflictedUnit = conflictedUnit;
    }
}