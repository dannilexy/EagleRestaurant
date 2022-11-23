﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topic);
    }
}
