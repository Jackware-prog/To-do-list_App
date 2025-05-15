
## Key Features

1. **Clean Architecture Layers**:
   - `Core`: Domain entities
   - `Infrastructure`: Data access implementations
   - `Application`: Business logic services
   - `API`: Presentation layer

2. **Testing Strategy**:
   - Unit tests for services (`Application.Tests`)
   - Integration tests for repositories (`Infrastructure.Tests`)
   - API contract tests (`API.Tests`)

3. **Frontend/Backend Separation**:
   - Client code completely isolated in `to-do-list_client`
   - API serves as backend interface

```mermaid
graph TD
    A[Root] --> B[.github]
    A --> C[To-do-list_API]
    A --> D[To-do-list_API.Tests]
    A --> E[To-do-list_Application]
    A --> F[To-do-list_Application.Tests]
    A --> G[to-do-list_client]
    A --> H[To-do-list_Core]
    A --> I[To-do-list_Infrastructure]
    A --> J[To-do-list_Infrastructure.Tests]
