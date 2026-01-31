namespace StudentResourcesDirectory.Common
{
    public class EntityValidation
    {
        /* Resource */
        public const int ResourceTitleMinLength = 2;
        public const int ResourceTitleMaxLength = 60;
        public const int ResourceDescriptionMinLength = 5;
        public const int ResourceDescriptionMaxLength = 500;

        /* Category */
        public const int CategoryNameMinLength = 2;
        public const int CategoryNameMaxLength = 50;

        /* Student */
        public const int StudentFirstNameMinLength = 2;
        public const int StudentFirstNameMaxLength = 30;
        public const int StudentLastNameMinLength = 2;
        public const int StudentLastNameMaxLength = 30;
    }
}