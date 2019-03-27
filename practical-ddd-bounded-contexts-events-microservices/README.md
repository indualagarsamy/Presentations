## Practical DDD: Bounded Contexts + Events => Microservices

Domain Driven Design and Messaging go hand in hand, like a warm chocolate brownie paired with vanilla ice-cream! DDD is a software discipline that allows you to move faster and write high-quality code. The whole point is to align the software you write to be flexible with the business changes. When you start to use the technology of messaging to communicate between clean and well-defined bounded contexts you get to remove temporal coupling. And voila, you now have microservices that are built for autonomy from the ground up. Sounds perfect?

In this talk, discover this intersection of DDD as a software discipline with messaging as a technology counterpart. Build reliable systems that can scale with the business changes.

## Demo

The code example uses [NServiceBus](https://docs.particular.net/nservicebus/) for messaging and the [Saga feature](https://docs.particular.net/nservicebus/sagas/). 

### First Model

The [First Model](https://github.com/indualagarsamy/Presentations/tree/master/practical-ddd-bounded-contexts-events-microservices/src/first-model) folder contains an example of a first try at modeling

### Refactoring

The [Refactored Model](https://github.com/indualagarsamy/Presentations/tree/master/practical-ddd-bounded-contexts-events-microservices/src/refactored-model) uses better names for the messages, class names and uses immutable messages.



