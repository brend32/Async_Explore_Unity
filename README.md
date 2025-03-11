# Async/Await vs Coroutine
Comparing possibilities of Async/Await and coroutines in Unity.

## Problem
By default CPU executes all instructions one after another. But not all 
operations can be completed immediately, for example: WebRequest, Asset loading, file writing etc.<br>
Without tricks a program freezes when it waits for operation to complete.

## Concurrency
First trick is to split operation into small parts and execute them by a little. We can execute small part and return to more important work like rendering frame.

## Parallel
Another trick is to move waiting or work into another thread. Threads are executed simultaneously by hardware (if possible, otherwise system fallbacks to concurrency where it is not enough cores to do it).

## Difference
In concurrent version execution of one task can affect other tasks. For example when task performing computations.<br>
In parallel version execution of one task doesn’t affect other tasks. So computations will be completed faster.

We can use both approaches to get the best results.

More:
https://docs.google.com/presentation/d/1Y21uhl8PBEBAzLwvMI4ZGuIRePynEJOem6O5AzgBSo8/edit#slide=id.g33de463a3bc_0_95
