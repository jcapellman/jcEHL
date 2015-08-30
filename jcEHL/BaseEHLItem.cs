namespace jcEHL {
    public class BaseEHLItem<T> {
        public BaseEHLItem(T baseObject) {
            if (baseObject == null) {
                return;
            }

            Copy.Init(baseObject, this);
        }    
    }
}