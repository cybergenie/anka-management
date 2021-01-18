using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Anka2.MVVM
{
    public class CommandObject:ICommand
    {
        /// <summary>
        /// 检查命令是否可以执行的事件，在UI事件发生导致控件状态或数据发生变化时触发
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// 判断命令是否可以执行的方法
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// 命令需要执行的方法
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// 创建一个命令
        /// </summary>
        /// <param name="execute">命令要执行的方法</param>
        public CommandObject(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// 创建一个命令
        /// </summary>
        /// <param name="execute">命令要执行的方法</param>
        /// <param name="canExecute">判断命令是否能够执行的方法</param>
        public CommandObject(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <param name="parameter">命令传入的参数</param>
        /// <returns>是否可以执行</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                _execute(parameter);
            }
        }
    }
    public class CommandObject<T> : ICommand
    {
        /// <summary>
        /// 检查命令是否可以执行的事件，在UI事件发生导致控件状态或数据发生变化时触发
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// 判断命令是否可以执行的方法
        /// </summary>
        private Func<T, bool> _canExecute;

        /// <summary>
        /// 命令需要执行的方法
        /// </summary>
        private Action<T> _execute;

        /// <summary>
        /// 创建一个命令
        /// </summary>
        /// <param name="execute">命令要执行的方法</param>
        public CommandObject(Action<T> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// 创建一个命令
        /// </summary>
        /// <param name="execute">命令要执行的方法</param>
        /// <param name="canExecute">判断命令是否能够执行的方法</param>
        public CommandObject(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <param name="parameter">命令传入的参数</param>
        /// <returns>是否可以执行</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute((T)parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                _execute((T)parameter);
            }
        }
    }

    public class EventCommand : TriggerAction<DependencyObject>
    {

        /// <summary>
        /// 事件要绑定的命令
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MsgName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(EventCommand), new PropertyMetadata(null));

        /// <summary>
        /// 绑定命令的参数，保持为空就是事件的参数
        /// </summary>
        public object CommandParateter
        {
            get { return (object)GetValue(CommandParateterProperty); }
            set { SetValue(CommandParateterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParateter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParateterProperty =
            DependencyProperty.Register("CommandParateter", typeof(object), typeof(EventCommand), new PropertyMetadata(null));

        //执行事件
        protected override void Invoke(object parameter)
        {
            if (CommandParateter != null)
                parameter = CommandParateter;
            var cmd = Command;
            if (cmd != null)
                cmd.Execute(parameter);
        }
    }
}
