namespace dang
{
    public interface IStateMachine
    {
        void ChangeState(EnumState newState);
        void Update();
    }

}
