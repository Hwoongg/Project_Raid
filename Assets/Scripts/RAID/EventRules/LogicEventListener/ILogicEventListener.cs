public interface ILogicEventListener<Ty>
{
    void OnEventRaised(Ty item);
};
