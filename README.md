# NCore
Opinionated library for creating CRUD LOB API's

## The philosophy
Trying to cram all the ideas and lessons learned into one lean framework for creating applications without spending too much time on the plumbing that is so essential.

The framework is loosely based on concepts like:
* Evans' Domain-Driven Design
* Aspect-oriented programming
* The SOLID principles
* Palermos Onion Structure
and perhaps the most important principle - use the right tool for the job and stand on the shoulders of giants.

## The reason
Having written basically the same code thrice - maybe it is time to just make the damn framework.

## The contents
With this framework you can build REST-full APIs for your line-of-business CRUD applications. Validation, audit trails and easy access to full-fledged search is all baked in.

## The frameworks
To avoid too many dependencies, light, performant frameworks are chosen over heavy swiss-army knife style tools. 

* NHibernate, because it does not interfere (too) much with the domain model.
* Nancy, because it better supports light-weight and flexible REST interfaces.
* FluentMigrator, because it supports production scenarios really well.
* NUnit, basically it does the work.
* Rhino.Mocks, testing AAA style is nicely supported..

## The data stores
* MS SQL - primary data store (at some point, maybe we will make this optional or switchable)
* Elastic Search - you know, for search
