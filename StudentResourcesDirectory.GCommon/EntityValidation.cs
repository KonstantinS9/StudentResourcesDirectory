namespace StudentResourcesDirectory.GCommon
{
    public class EntityValidation
    {
        public static class Resource
        {
            public const int ResourceTitleMinLength = 2;
            public const int ResourceTitleMaxLength = 60;
            public const int ResourceDescriptionMinLength = 5;
            public const int ResourceDescriptionMaxLength = 500;
        }

        public static class Category
        {
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 50;
        }

        public static class Student
        {
            public const int StudentFirstNameMinLength = 2;
            public const int StudentFirstNameMaxLength = 30;
            public const int StudentLastNameMinLength = 2;
            public const int StudentLastNameMaxLength = 30;
        }

        public static class Comment
        {
            public const int CommentContentMinLength = 1;
            public const int CommentContentMaxLength = 1000;
        }
    }
}