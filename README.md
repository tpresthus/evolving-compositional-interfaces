# Evolving compositional user interfaces

This is the source code repository for the demo application used in the
NDC 2019 talk [Evolving compositional user interfaces][1].

## Description

Ever since we started breaking applications into services, be it in the
era of SOA or more recently with microservices, we’ve struggled to incorporate
user interfaces into our decoupled, distributed architectures. We’ve seen
frontends versioned separately with tight coupling to our services, breaking
cohesion. We’ve seen the rise of Backend-For-Frontend and the emerge of micro
frontends. We talk about composition, yet so many projects fail to implement
actual composition. Instead we end up with some kind of compromise, with
repeated business logic in the front-end, back-end and API, making it hard to
scale – especially when multiple teams are involved – causing lock-step
deployment, latency, bottlenecks and coordination issues.

What if we could find a viable solution that allowed us to scale development,
keep distribution and cohesion and also provide composition of user interfaces?
In this talk you are introduced to the evolution of compositional user
interfaces and existing patterns while we discover their pros and cons, before
diving into the architecture and development of compositional interfaces using
hypermedia and micro-frontends. We go beyond the simple “Hello World” example
that always seems to work, and you’ll learn patterns in modelling and design
that will get you up and running with decoupled, composed user interfaces in
your day job.

  [1]: https://ndcoslo.com/talk/evolving-compositional-user-interfaces/