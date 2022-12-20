using HallWebApi.model.dto;
using MassTransit;
using PickyBride.princess;

namespace PickyBride.consumer;

public class ContenderConsumer : IConsumer<ContenderNameDto>
{
    private Princess _princess;
    
    public ContenderConsumer(Princess princess)
    {
        _princess = princess;
    }
    
    /// <summary>
    /// Consumes contender name from RebbitMQ
    /// if hall has new contenders, than Contender full name = {name patronymic}. 
    /// if hall has not any new contender, than null.
    /// Call method in princess for getting a new contender
    /// </summary>
    /// <param name="context"></param>
    public async Task Consume(ConsumeContext<ContenderNameDto> context)
    {
        var message = context.Message;
        await _princess.ReceiveNewContender(message.Name);
    }
}