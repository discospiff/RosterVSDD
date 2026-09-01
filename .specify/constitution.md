# RosterVSDD Constitution

## Core Principles

### I. Code Quality & Maintainability
- Follow idiomatic C# and .NET 10 best practices (SOLID, small functions, explicit interfaces).
- Use consistent formatting and naming conventions enforced by editorconfig and dotnet-format.
- Enable Roslyn analyzers and address warnings; treat analyzer failures and build warnings as first-class issues.
- Prefer explicit, well-documented public APIs; keep implementation details internal to modules.

### II. Test-First (NON-NEGOTIABLE)
- Adopt Spec-Driven Development: write/specify tests (unit/spec/integration) before implementation.
- Maintain automated unit, integration, and acceptance tests. Unit tests must be fast and deterministic.
- Target minimum code coverage thresholds per project (configurable); failing tests block merge to main.
- Use test doubles (mocks/fakes) for external systems; prefer contract tests for cross-service contracts.

### III. User Experience Consistency & Accessibility
- Razor Pages UI must follow shared layout and component patterns for consistent look-and-feel.
- Enforce accessibility standards (WCAG 2.1 AA) and responsive design across supported viewports.
- Error states and validation must be clear, localizable, and consistent across pages.
- Prioritize simple, discoverable flows and guardrails that prevent destructive actions.

### IV. Performance & Scalability
- Define performance budgets: server response time (p95) and critical page load budgets; measure and track regressions.
- Use async I/O across request paths, efficient data access patterns, and cache where appropriate.
- Protect against N+1 queries and unbounded memory growth; monitor with telemetry and alerts.
- All changes with potential perf impact require profiling evidence and load test results before merging.

## Additional Constraints
- Target platform: .NET 10 and Razor Pages unless a clear, documented reason exists to deviate.
- Use proven, actively maintained libraries; prefer Microsoft or widely adopted OSS packages.
- Secrets and PII must never be committed. Use secure stores and follow company security guidelines.

## Development Workflow & Quality Gates
- All work occurs on feature branches and opens a pull request against main.
- PRs require at least one approving code review and passing CI that runs static analysis, tests, and security scans.
- Merge is allowed only when: tests pass, required reviewers approve, and the branch is up-to-date with main.

## Observability & Diagnostics
- Emit structured logs and correlation IDs for requests. Instrument key business flows with metrics and traces.
- Add meaningful telemetry for errors, performance, and availability; keep costs and cardinality under control.

## Governance
- This constitution guides development practices. Amendments require a documented rationale and at least one approval from the maintainers team.
- Compliance verification: CI checks and PR reviewers will validate adherence to these principles.

**Version**: 1.0 | **Ratified**: [RATIFICATION_DATE] | **Last Amended**: [LAST_AMENDED_DATE]
