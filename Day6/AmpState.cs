namespace Day6 {
    public class AmpState {
        public int IndexState { get; set; }
        public int[] MemoryState { get; set; }
        public int Output { get; set; }

        public bool Paused { get; set; }
        public bool Stopped { get; set; }

    }
}