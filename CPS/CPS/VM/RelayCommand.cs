using System;
using System.Windows.Input;

namespace CPS.VM
{
    /// <summary>
    /// A generic command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'. This class allows you to accept command parameters in the
    /// Execute and CanExecute callback methods.
    /// </summary>
    /// <remarks>The <see cref="CommandManager"/>handles automatic enabling/disabling of controls based on the CanExecute delegate.</remarks>
    public class RelayCommand : ICommand
    {

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/>  class that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic encapsulated by the <paramref name="execute"/> delegate. </param>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="execute"/> argument is null.</exception>
        public RelayCommand(Action execute) : this(execute, null) { }
        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="canExecute">The execution status logic encapsulated by the <paramref name="canExecute"/> delegate.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;
        }
        #endregion

        #region ICommand
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. Because the command does not require data 
        /// to be passed, this parameter is always ignored</param>
        /// <returns><c>true</c> if this command can be executed; otherwise, <c>false</c>.</returns>
        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
                return true;
            if (parameter == null)
                return this._canExecute();
            return this._canExecute();
        }
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. Because the command does not require data 
        /// to be passed, this parameter is always ignored</param>
        public virtual void Execute(object parameter)
        {
            this._execute();
        }
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        #endregion

        #region API
        /// <summary>
        /// Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        internal void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region private
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        #endregion
    }
}
