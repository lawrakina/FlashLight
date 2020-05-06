using FpsUnity.Interface;


namespace FpsUnity.Controller
{
    public sealed class PlayerController : BaseController, IExecute
    {
        #region Fields

        private readonly IMotor _motor;

        #endregion


        #region UnityMethods

        public PlayerController(IMotor motor)
        {
            _motor = motor;
        }

        public void Execute()
        {
            if (!IsActive) { return; }
            _motor.Move();
        }

        #endregion
    }
}