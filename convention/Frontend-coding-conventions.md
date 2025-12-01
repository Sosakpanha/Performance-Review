# Front-End Development Guidelines

## Component Structure and Organization

### Directory Structure
- Components must be defined in `packages/ui/components/` folder
- Export components in `packages/ui/components/index.ts` if needed for external use

```
└── components/
    ├── desktops/
    │   ├── DesktopLanguageSelection.vue
    │   ├── DesktopFooter.vue
    │   └── headers/
    │       ├── DesktopModernHeader.vue
    │       └── style-one/
    │           ├── DesktopHeaderStyleOne.vue
    │           ├── DesktopHeaderStyleOneMenu.vue
    │           └── DesktopHeaderStyleOneTopActions.vue
    └── mobiles/
        ├── MobileLanguageSelection.vue
        ├── MobileFooter.vue
        └── headers/
            ├── MobileModernHeader.vue
            └── style-one/
                ├── MobileHeaderStyleOne.vue
                ├── MobileHeaderStyleOneMenu.vue
                └── MobileHeaderStyleOneTopActions.vue
```

### Component Naming Conventions

- Use meaningful and descriptive names (e.g., `LanguageSelection.vue` instead of `Language.vue`)
- Always use PascalCase for component filenames (e.g., `MemberMessage.vue` instead of `member-message.vue`)

#### Naming Patterns

| Pattern | Description | Example |
|---------|-------------|---------|
| `ComponentName.vue` | Global component for any theme and device | `TopHeader.vue` |
| `ThemeComponentName.vue` | Specific theme, any device | `MainLanguageSelection.vue` |
| `[Desktop\|Mobile]ComponentName.vue` | Any theme, specific device | `DesktopMemberMessage.vue` |
| `[Desktop\|Mobile]ThemeComponentName.vue` | Specific theme and device | `MobileMainHeader.vue` |

## Vue Component Structure

### Template Tag Best Practices

- Avoid complex logic in templates
- Move conditional logic and event handlers to script section

```vue
<!-- Bad -->
<button
  v-if="authenticated"
  class="cursor-pointer"
  @click="
    game.GameId === 6493
      ? handleGameEntryOpenWindow(game, -1, -1, EnumEntranceLocation.GameLobby)
      : handleGameEntryOpenWindow(game, 850, 850, EnumEntranceLocation.GameLobby)
  "
>Play Game</button>

<!-- Good -->
<button
  v-if="authenticated"
  class="cursor-pointer"
  @click="onGameClicked(game)"
>Play Game</button>

<script setup lang="ts">
const onGameClicked = (game: IBsiGame) => {
  const MAGIC_NUMBER_HERER = 6493;
  if (game.GameId === MAGIC_NUMBER_HERER) {
    handleGameEntryOpenWindow(game, -1, -1, EnumEntranceLocation.GameLobby);
    return;
  }
  handleGameEntryOpenWindow(game, 850, 850, EnumEntranceLocation.GameLobby);
};
</script>
```

### Script Tag Requirements

- Must use Composition API with `<script setup lang="ts">`
- TypeScript is required (`lang="ts"`)

```vue
<!-- Correct -->
<template>
  <div>{{ locale }}</div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const locale = ref('');
</script>

<!-- Incorrect - Do not use Options API -->
<template>
  <div>{{ locale }}</div>
</template>

<script lang="ts">
import { ref } from 'vue';

export default {
  name: "TestComponent",
  setup() {
    const locale = ref('');

    return {
      locale,
    };
  },
};
</script>
```

### Style Tag Guidelines

- Use Tailwind utility classes whenever possible
- Apply Tailwind utilities to custom styles with `@apply`
- Styles must use `scoped` attribute

```vue
<!-- Correct -->
<style scoped lang="scss">
.message-count {
  color: var(--text);
  font-family: Roboto;

  @apply text-[12px] font-bold text-center whitespace-nowrap;
}
</style>

<!-- Incorrect - Not using Tailwind where possible -->
<style scoped lang="scss">
.message-count {
  color: var(--text);
  font-family: Roboto;
  font-size: 12px;
  font-weight: 700;
  letter-spacing: 0em;
  text-align: center;
  text-wrap: nowrap;
}
</style>
```

## Component API Definitions

### Emits Definition

- Use type-only emit declarations

```typescript
// Correct
const emit = defineEmits<{
  change: [id: number] // named tuple syntax
  update: [value: string]
}>();

// Incorrect
const emit = defineEmits(['change', 'update']);
```

### Props Definition

- Use type-only prop declarations

```typescript
// Correct
const props = defineProps<{
  foo: string
  bar?: number
}>()

// With default values
interface Props {
  msg?: string
  labels?: string[]
}

const props = withDefaults(defineProps<Props>(), {
  msg: 'hello',
  labels: () => ['one', 'two']
})

// Incorrect
const props = defineProps({
  foo: String
})
```

## Variables and State Management

### Variables Definition

- Use `const` for constants and `let` for variables that will change
- Declare variables with explicit types when necessary
- Provide default values for ref variables

```typescript
// Correct
const fixedValue: number = 42;
let changingValue: number = 10;

// State variables in Composition API
const count = ref<number>(0);
const gameProviders = ref<IGameProvider[]>([]);

// Incorrect
var fixedValue: number = 0;
const count = ref(); // No type and no default value
const gameProviders = ref<IGameProvider[]>(); // No default value
const count = ref<any>(0); // Using any type
```

### Naming Conventions

- Use camelCase for variable names
- Constants should be stored in `constants.ts`
  - Constant names must use SCREAMING_SNAKE_CASE (e.g., `LANGUAGE_COOKIE_KEY`)
  - Constant values must use kebab-case (e.g., `data-server-endpoint`)

```typescript
// Correct
const gameProviders = ref<IGameProvider[]>([]);

// Incorrect
const GameProviders = ref<IGameProvider[]>([]);
const game_providers = ref<IGameProvider[]>([]);
```

## Using Components

### Import and Reference

- Use manual import instead of auto-import
- Use PascalCase when referencing components in templates
- Use kebab-case for props

```typescript
<!-- Correct -->
<template>
  <MessagesMessageItem :is-unread="activeTab === 'Unread'" />
</template>

<script setup lang="ts">
import MessagesMessageItem from "./MessageItem.vue";
</script>

<!-- Incorrect -->
<template>
  <messages-message-item :isUnread="activeTab === 'Unread'" />
</template>
```

## UI Components and Libraries

### Element UI Pagination

- Use Element UI for pagination components
- Configure pagination with essential props and v-model directives
- Apply proper attribute casing (kebab-case for attributes)

```vue
<!-- Correct -->
<el-pagination
  v-model:current-page="currentPage"
  v-model:page-size="pageSize"
  :page-sizes="[10, 25, 50, 100]"
  :total="filterTableData.length"
  layout="prev, pager, next, sizes"
  class="mt-2 md:mt-0"
/>

<!-- Incorrect - Missing v-model directives -->
<el-pagination
  :current-page="currentPage"
  :page-size="pageSize"
  :total="filterTableData.length"
  layout="prev, pager, next, sizes"
/>
```

### Element UI Best Practices

- Import components individually instead of globally registering all components
- Use Tailwind classes for spacing and responsive design
- Combine Element UI props with Tailwind utility classes for consistent styling

## Composables

### Organization and Naming

- Composable files can be nested inside folder only two levels deep
  ```
  composables/
  |-- sports
  |   |-- useRecommendMatch.ts
  |   |-- useLiveMatch.ts
  |-- bet-history
      |-- usePlayerSportBetHistory.ts
      |-- usePlayerLiveCasinoBetHistory.ts
  ```
- File name and function name must start with "use"
- One file must have only one exported composable function with the same name as the file

```typescript
// useObjectCloned.ts
function cloneFnJSON<T>(source: T): T {
  return JSON.parse(JSON.stringify(source))
}

// This function is considered as the composable function
export function useObjectCloned<T>(
  source: MaybeRefOrGetter<T>,
  options: UseClonedOptions = {},
): UseClonedReturn<T> {
  // Implementation...
}
```

### Best Practices

- Reuse composables when possible, making them reducible
- Avoid calling APIs directly inside composables unless necessary

## Type and Interface Imports/Exports

### Import Syntax for Types

- Use verbatim module import to improve tree-shaking

```typescript
// Correct - Type-only import
import type { IThemePropertiesFromApi } from "@/models/companyThemePropertiesRespone";

// Correct - Mixed import with type and class
import {
  type IThemePropertiesFromApi,
  ThemePropertiesFromApiDto, // this is a class
} from "@/models/companyThemePropertiesRespone";

// Incorrect - Old way without type keyword
import { IThemePropertiesFromApi } from "@/models/companyThemePropertiesRespone";
```

## Error Handling and Logging

### Try-Catch and Logging

- Use the `useAppLogger()` composable for manual logging
- Handle errors properly in try-catch blocks

```typescript
const logger = useAppLogger();

const updateCustomerInfoTelegram = async (customerId: number) => {
  try {
    // Implementation...
  }
  catch (error) {
    // Proper error logging
    const _error = error instanceof Error ? error : new Error(`${error}`);
    logger.error({
      messageName: `updateCustomerInfoTelegram: ${_error.name}`,
      message: _error.message,
      stack: _error.stack,
    });
    notificationHelper.notification("Failed Update", EnumMessageType.Error);
  }
};
```

## Data Handling

### IDs and Unique Identifiers

- Be aware that some IDs (like `CustomerId`) may be unique in one context but not another
- Use composite keys when necessary to ensure uniqueness

### Image Naming Conventions

- Use consistent casing (all lowercase) for image URLs

## Interfaces, Classes, and Enums

### Interfaces

- Interfaces must be stored in the `models` folder
- Interface names should start with "I" (e.g., `IGame`)
- Don't store interfaces in composables

### Classes

- Classes must be in the `models` folder
- Class names must use PascalCase (e.g., `GameProviderList`)
- Classes should implement from interfaces when applicable

### Enums

- Enums must be stored in the `enums` folder
- Enum file names must use camelCase
- Enum names must use PascalCase
- Use named exports, not default exports