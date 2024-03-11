using LogForU.Core.Enums;
using LogForU.Core.Models;
using LogForU.Core.Layouts.Interfaces;

namespace LogForU.Core.Appenders.Interfaces;

public interface IAppender
{
    ILayout Layout { get; }

    ReportLevel ReportLevel { get; set; }

    int MessagesAppended { get; }

    void AppendMessage(Message message);
}