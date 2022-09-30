
namespace ConsultaAPICodeFirst.Exceptions
{
    public class ConstraintException : System.Exception
    {
        //Classe criada para tratar as exceptions de constraint

        public ConstraintException() { }
        public ConstraintException(string message) : base(message) { }
        public ConstraintException(string message, System.Exception inner) : base(message, inner) { }
        protected ConstraintException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

