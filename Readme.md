# Messenger Backend

A fast, minimal ASP.NET Core backend that powers a real-time chat application.

The server exposes a SignalR hub for live communication and provides lightweight endpoints for room and user management. 

Also uses compiled React frontend from https://github.com/kuninsoft/signal-chat-client

## Features
- Real-time messaging powered by SignalR
- Room join/leave flow with event broadcasting
- Minimal API architecture for clean and simple routing
- Strongly typed request/response models
- Dedicated services for managing users and rooms
- Graceful connection handling inside the SignalR hub

## Challenges
- Designing a clean minimal-API structure with proper DI
- Managing user lifecycle events inside SignalR hubs
- Keeping the message broadcasting logic stateless and scalable
- Passing frontend context (username/room) into hub connection state

## Tech Stack
- ASP.NET Core
- SignalR
- Minimal APIs
- Dependency Injection
- C#
- In-memory state management
