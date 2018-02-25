namespace PhotoSorterWPF.Commands
{
    public interface ICommand<T>
    {
        void Execute(T parameter);
    }
}
