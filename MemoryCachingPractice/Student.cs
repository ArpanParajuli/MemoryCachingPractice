namespace MemoryCachingPractice
{
    public class Student
    {
        public int Id { get; set; }
        public Guid Pid { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
