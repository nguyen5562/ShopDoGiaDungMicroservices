namespace ProductServices.DTO
{
    public class PermissionDto
    {
        public string FunctionCode { get; set; }
        public string ActionCode { get; set; }

        public bool Equals(PermissionDto other)
        {
            if (other == null)
                return false;

            return FunctionCode == other.FunctionCode && ActionCode == other.ActionCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PermissionDto);
        }

        public override int GetHashCode()
        {
            return (FunctionCode + ActionCode).GetHashCode();
        }
    }
}
