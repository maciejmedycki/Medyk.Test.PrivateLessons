namespace Medyk.Test.PrivateLessons
{
    public interface IDataValidator
    {
        public bool Disabled { get; set; }

        bool IsValid(object data);

        string GetValidationMessage(object data);
    }
}