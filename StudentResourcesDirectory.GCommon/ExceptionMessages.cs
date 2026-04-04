
namespace StudentResourcesDirectory.GCommon
{
    public static class ExceptionMessages
    {
        public static class Resource
        {
            public const string InvalidStudentIdForCreatingResource = "No student profile found for this user with id: {0}.";
            public const string ResourceNotFound = "Resource not found.";
            public const string NotOwnerOfResource = "Not owner.";
        } 

        public static class Comment
        {
            public const string CommentNotFound = "Comment not found.";
            public const string NotOwnerOfComment = "You are not the owner of the comment!";
            public const string InvalidUserId = "User Id does not exist";
        }

        public static class Rating
        {
            public const string InvalidUserId = "User Id does not exist";
            public const string RatingNotFound = "Rating not found.";
            public const string YouGaveRatingAlready = "You gave rating already.";
        }

        public static class Student
        {
            public const string StudentNotFound = "Student not found.";
        }
    }
}