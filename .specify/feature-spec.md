# Feature Specification: Roster

**Feature Branch**: `feature/roster`

**Created**: 2026-09-01

**Status**: Draft

**Input**: User description: "The roster should be a web application where, on a form on the home page, users can enter their first name, last name, major, favorite keyboard shortcut, and where that shortcut can be used. Once submitted, this information should be stored in memory. The home page should also show a list of this same information for all users who have already filled out the form. The home page should have a link to a separate page that shows a JSON feed of this same information. This application should be able to run locally in Visual Studio 2026, and will be deployed and run in Azure."

## User Scenarios & Testing (mandatory)

### User Story 1 - Submit roster entry (Priority: P1)

As a user, I can fill out a form on the home page with First Name, Last Name, Major, Favorite Keyboard Shortcut, and Shortcut Context, submit it, and see my entry added to the roster list on the home page.

Why this priority: This is the primary interaction and minimum viable value — collecting roster entries.

Independent Test: Fill form locally in the browser and verify the list updates and retains the entry in-memory during the app lifetime.

Acceptance Scenarios:
1. Given the app is running and there are zero or more existing entries, when the user fills all required fields and submits, then the form submission succeeds (HTTP 200/redirect), the new entry appears in the roster list on the home page, and the input fields are cleared.
2. Given a submitted entry with all fields present, when the page is refreshed, then the in-memory list still contains the entry for the lifetime of the process.
3. Given a submission with any missing required field, when the user submits, then form validation prevents submission and displays field-level validation messages.

---

### User Story 2 - View roster list on home page (Priority: P1)

As a user, I can visit the home page and see the roster list containing previously submitted entries.

Why this priority: The roster list is the read-side of the feature required to verify stored entries.

Independent Test: Start the application with seeded or previously added entries and verify the list renders each entry with all fields.

Acceptance Scenarios:
1. Given the app has zero entries, when a user visits the home page, then a friendly empty-state message is shown (e.g., "No entries yet").
2. Given the app has one or more entries, when a user visits the home page, then each entry is displayed showing First Name, Last Name, Major, Shortcut, and Shortcut Context.

---

### User Story 3 - JSON feed (Priority: P2)

As a developer or consumer, I can request the JSON feed at GET /api/roster and receive the current roster as application/json.

Why this priority: Useful for integration and automation; required by the product description but not the primary UI.

Independent Test: Call GET /api/roster locally and verify the response is 200 and contains a JSON array of roster entries with the expected shape.

Acceptance Scenarios:
1. Given zero entries, when GET /api/roster is called, then response is 200 with an empty JSON array [] and Content-Type application/json.
2. Given entries exist, when GET /api/roster is called, then response is 200 with a JSON array where each object has the fields: firstName, lastName, major, favoriteKeyboardShortcut, shortcutContext.

---

### Edge Cases

- Submitting unusually long values (e.g., 5,000 characters) should be constrained by sensible field length limits and produce validation errors when exceeded.
- Submitting markup or script-like content should be rendered escaped in the UI to prevent XSS.
- Memory growth: since persistence is in-memory, the app must handle or limit the number/size of entries and expose a clear note that data is volatile.
- Concurrent submissions: concurrent users submitting entries should not corrupt in-memory state.
- API consumers expect stable field names and JSON structure; breaking changes must be versioned.

## Requirements (mandatory)

### Functional Requirements

- FR-001: System MUST present a home page (/) with a form that accepts First Name, Last Name, Major, Favorite Keyboard Shortcut, and Shortcut Context.
- FR-002: All five fields in the form are REQUIRED. Client-side and server-side validation must enforce this.
- FR-003: System MUST persist submissions in-memory for the lifetime of the running process.
- FR-004: Home page MUST display a roster list showing all stored entries with their fields.
- FR-005: System MUST expose a JSON API endpoint GET /api/roster that returns the current roster as application/json.
- FR-006: System MUST run locally in Visual Studio 2026 and target .NET 10.
- FR-007: System MUST be deployable to Azure (App Service or similar) without code changes beyond configuration.
- FR-008: System MUST enforce basic accessibility (labels, ARIA where appropriate) and responsive layout on the home page.
- FR-009: System MUST escape HTML in displayed values to prevent XSS.
- FR-010: System SHOULD limit maximum field length (e.g., 256 chars for names/major, 128 chars for shortcuts and contexts) and return a clear validation error when exceeded.

### Non-Functional Requirements

- NFR-001: Server-side submission should complete quickly (target <200ms under light load).
- NFR-002: Use structured logging and include correlation IDs for requests.
- NFR-003: Follow the project constitution: tests written before implementation (spec-driven), automated tests added, and code review required before merging.

### Key Entities

- RosterEntry: Represents a single submission with attributes:
  - firstName: string (required)
  - lastName: string (required)
  - major: string (required)
  - favoriteKeyboardShortcut: string (required)
  - shortcutContext: string (required)
  - createdUtc: DateTime (server-generated)

## Success Criteria (mandatory)

### Measurable Outcomes

- SC-001: Developers can run the app locally in Visual Studio 2026 and open the home page at /.
- SC-002: A user can submit a valid roster entry and see it appended to the roster list immediately.
- SC-003: GET /api/roster returns the current roster as JSON with the expected schema; tests verify the JSON shape.
- SC-004: Form validation prevents submission when required fields are missing and displays errors.
- SC-005: Basic accessibility checks (labels, keyboard navigation) pass for the home page form.
- SC-006: Unit tests and at least one integration test exist for the submission flow and JSON endpoint.

## Assumptions

- App authentication and authorization are out-of-scope for v1; the roster is publicly writable and readable.
- Persistence is explicitly in-memory and volatile; no database is required for v1.
- Deployment target is Azure App Service; deployment will be via standard CI/CD with configuration for environment variables.
- Local development will use Kestrel and IIS Express as supported by Visual Studio 2026.

## Notes

- If long-term storage or privacy controls are later required, update the spec to add persistence and access control.
- Field length limits and exact numeric budgets (e.g., max entries in memory) can be tuned after an initial implementation and profiling.
