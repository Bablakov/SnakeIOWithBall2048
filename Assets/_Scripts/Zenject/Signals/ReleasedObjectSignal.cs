public class ReleasedObjectSignal<TypeObject> {
    public readonly TypeObject ReleasedObject;
    public readonly bool NeedSpawnRepeat;

    public ReleasedObjectSignal(TypeObject releaseObject, bool needSpawnRepeat = false) {
        ReleasedObject = releaseObject;
        NeedSpawnRepeat = needSpawnRepeat;
    }
}