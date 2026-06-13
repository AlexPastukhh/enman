// ==UserScript==
// @name         Reusable Chat Command Helper
// @namespace    https://github.com/AlexPastukhh/enman/reusable-docs
// @version      0.5.0
// @description  Reusable list-only draggable command helper for inserting structured command prompt bodies into ChatGPT.
// @author       Reusable docs layer
// @match        https://chatgpt.com/*
// @match        https://chat.openai.com/*
// @run-at       document-idle
// @grant        none
// ==/UserScript==

/*
TM-REUSE-ONLY source sync:
  Source-of-truth:
    - planning/planning-use-case-map.md @ Doc version: v1.1.0
    - planning/documentation/tampermonkey-command-projection-workflow.md @ Doc version: v0.2.0
    - planning/documentation/tools/tampermonkey/README.md @ Doc version: v0.2.0
    - planning/documentation/field-kits/root-use-case-map-field-kit.md @ Doc version: v0.2.0

  Boundary:
    - This userscript is a reusable documentation-layer helper projection.
    - It is not command source of truth.
    - Command semantics remain owned by the project root use-case map and linked owner workflow/example files.
    - The current command set is a reusable/common seed. A copied project may trim/adapt commands after its UCM routes are accepted.
*/


(function () {
  'use strict';

  const COMMANDS = [
  {
    "id": "replacement_archive.create",
    "group": "MVP-1",
    "label": "давай архив",
    "description": "output package",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  давай архив\n\nenglish_name:\n  give arch\n\ncommand_family:\n  `давай архив` / `собери архив` / `replacement package`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Output-package mode, not archive read-source mode.\n  - Use active approved scope; ask only blocking questions.\n  - Produce a full replacement archive.\n  - Do not use patches.\n  - Do not use patch files.\n  - Do not use scripts as the primary application mechanism.\n  - Do not provide a planning-only answer instead of the archive.\n  - Any response without a full replacement archive is incorrect for this command.\n  - Do not put apply commands only inside the archive.\n  - Give apply/diff commands in chat.\n  - Save full diff to file and copy it to clipboard.\n  - Ask user to paste diff before commit.\n  - Do not commit or push.\n\nuser_target:\n  <what archive/package should include>\n\n[/ENMAN_COMMAND]",
    "englishName": "give arch"
  },
  {
    "id": "replacement_archive.review_diff_file",
    "group": "MVP-1",
    "label": "давай архив с review diff file",
    "description": "repo diff package",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  давай архив с review diff file\n\nenglish_name:\n  give arch rev dif\n\ncommand_family:\n  `давай архив с review diff file` / `давай архив с repo diff` / `archive with review diff file`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read `planning/replacement-file-generation-guide.md` and `planning/documentation/review-diff-file-workflow.md`.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Explicit-only output-package mode, not default `давай архив`.\n  - Use only when repo-stored review diff transfer is requested/approved.\n  - Apply command may create/commit/push only `_ai-review-diffs/last-archive.diff`.\n  - Do not create `_ai-review-diffs/last-archive-summary.md` by default.\n  - Real archive files remain local until diff review approval.\n\nuser_target:\n  <what archive/package should include>\n\n[/ENMAN_COMMAND]",
    "englishName": "give arch rev dif"
  },
  {
    "id": "archive_source.use",
    "group": "MVP-1",
    "label": "арх",
    "description": "archive source",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  арх\n\nenglish_name:\n  added arch\n\ncommand_family:\n  `арх` / `из архива` / `use archive`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Read-source mode, not output-package mode.\n  - Use provided/latest archive as source snapshot.\n  - Do not create replacement archive unless separately requested.\n  - State archive freshness/source limits when relevant.\n\nuser_target:\n  <what should be checked from archive>\n\n[/ENMAN_COMMAND]",
    "englishName": "added arch"
  },
  {
    "id": "goal_map.sync",
    "group": "MVP-1",
    "label": "синх карта",
    "description": "map sync",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  синх карта\n\nenglish_name:\n  sync map\n\ncommand_family:\n  `синх карта` / `синхронизируй карту` / `синх карта архив`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Inspect the living Goal Map first.\n  - Output Goal Map Brief in synced target state.\n  - Create narrow map-sync archive in the same response.\n  - Include apply/diff commands in chat.\n  - Do not start the next functional slice.\n  - End with `План файл-обновление`.\n\nuser_target:\n  <goal/map target or current active workstream>\n\n[/ENMAN_COMMAND]",
    "englishName": "sync map"
  },
  {
    "id": "file_update.plan",
    "group": "MVP-1",
    "label": "план файл-обновление",
    "description": "file plan",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  план файл-обновление\n\nenglish_name:\n  plan file update\n\ncommand_family:\n  `план файл-обновление` / `спланируй обновление файлов` / `спланируй архив`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Plan file/docs/code/archive update only.\n  - End with `План файл-обновление` in planned mode.\n  - Include files, responsibilities, `Что`, `Почему`, boundaries, checks and next action.\n  - Do not edit files.\n  - Do not create archive unless separately requested.\n\nuser_target:\n  <what update/archive should be planned>\n\n[/ENMAN_COMMAND]",
    "englishName": "plan file update"
  },
  {
    "id": "critical_review.apply",
    "group": "MVP-1",
    "label": "крит",
    "description": "critical review",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  крит\n\nenglish_name:\n  crit\n\ncommand_family:\n  `крит` / `критически оцени` / `critical review`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Treat target as hypothesis, not accepted truth.\n  - Give honest verdict.\n  - Include strong points, weak points, risks, assumptions and alternatives.\n  - Do not disagree just to disagree.\n  - Do not edit files, create archives, commit or push.\n\nuser_target:\n  <what should be critically reviewed>\n\n[/ENMAN_COMMAND]",
    "englishName": "crit"
  },
  {
    "id": "plan.now",
    "group": "MVP-1",
    "label": "планируй",
    "description": "plan now",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  планируй\n\nenglish_name:\n  plan now\n\ncommand_family:\n  `планируй` / `распланируй` / `plan`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Plan now, do not defer.\n  - Use living Goal Map when active workstream exists.\n  - Choose concrete next slice/step.\n  - State scope, boundary, expected evidence and next action.\n  - Do not edit files or create archive unless separately requested.\n\nuser_target:\n  <what should be planned>\n\n[/ENMAN_COMMAND]",
    "englishName": "plan now"
  },
  {
    "id": "command.create",
    "group": "MVP-1",
    "label": "создай команду",
    "description": "new command",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  создай команду\n\nenglish_name:\n  create command\n\ncommand_family:\n  `создай команду` / `создай новую команду` / `добавь команду` / `спланируй команду` / `new command` / `create command`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read `planning/documentation/command-creation-workflow.md`, `planning/documentation/reviewable-agent-output-and-commands-workflow.md`, `planning/documentation/examples/README.md` and `planning/documentation/tampermonkey-command-projection-workflow.md` when Tampermonkey projection is in scope.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Create command semantics in UCM/owner docs first; Tampermonkey is projection, not source of truth.\n  - Define command family, command type, owner files, expected output and permission boundary.\n  - Use the UCM row template; do not copy a similar row blindly.\n  - Decide example coverage and Goal Map/workstream impact.\n  - Add/update Tampermonkey only when explicitly in scope.\n  - Do not edit files or create archive unless separately requested.\n\nuser_target:\n  <what command should be created or planned>\n\n[/ENMAN_COMMAND]",
    "englishName": "create command"
  },
  {
    "id": "parallel_workspace.start",
    "group": "MVP-1",
    "label": "начни параллельную работу",
    "description": "parallel workspace",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  начни параллельную работу\n\nenglish_name:\n  start parallel work\n\ncommand_family:\n  `параллельный агент` / `работай параллельно` / `parallel workspace` / `parallel work` / `создай parallel workspace` / `начни параллельную работу` / `старт параллельной работы` / `создай параллельный воркфлоу` / `start parallel workflow`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read `planning/documentation/parallel-work/README.md`, `planning/documentation/parallel-work/parallel-workflow.md` and `planning/documentation/parallel-work/PARALLEL-WORKSPACE-TEMPLATE.md`.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Start one staging-only workspace only for a concrete agent/workstream target.\n  - Record base snapshot, responsibility map, local action log and shadow copies.\n  - Do not create placeholder workspace or sync plan.\n  - Do not edit shared canonical docs directly from workspace phase.\n  - Canonical changes require later aggregate sync.\n\nuser_target:\n  <parallel agent/workstream target>\n\n[/ENMAN_COMMAND]",
    "englishName": "start parallel work"
  },
  {
    "id": "goal_map.brief",
    "group": "MVP-1",
    "label": "кц",
    "description": "goal brief",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  кц\n\nenglish_name:\n  gm brief\n\ncommand_family:\n  `кц` / `карта цели кратко` / `goal map brief`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Output compact Goal Map Brief.\n  - Current slice expanded.\n  - Other slices status-only.\n  - Use detailed slice statuses, not roadmap phase statuses.\n  - If map is stale, say so.\n\nuser_target:\n  <goal/map target or current active workstream>\n\n[/ENMAN_COMMAND]",
    "englishName": "gm brief"
  },
  {
    "id": "context_recheck.apply",
    "group": "MVP-1",
    "label": "обс",
    "description": "context recheck",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  обс\n\nenglish_name:\n  chat rech\n\ncommand_family:\n  `обс` / `перепроверь обсуждение` / `context recheck`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Re-check relevant prior discussion.\n  - Preserve accepted decisions and constraints.\n  - State what was checked and what remains unavailable.\n  - Combine with the underlying task route.\n\nuser_target:\n  <what discussion/context should be rechecked>\n\n[/ENMAN_COMMAND]",
    "englishName": "chat rech"
  },
  {
    "id": "source_register.scan_stale_versions",
    "group": "MVP-1",
    "label": "стейл версии",
    "description": "register stale",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  стейл версии в регистрах\n\nenglish_name:\n  scan register stale versions\n\ncommand_family:\n  `стейл версии в регистрах` / `проверь регистры на стейл версии` / `register stale version scan`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n    Then read `planning/source-cascade-sync-workflow.md` and the relevant source-sync register(s).\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Review/audit mode by default, not edit/package mode.\n  - Use bounded scope; ask for register/layer if no active scope is clear.\n  - Distinguish current register rows from historical Source Delta/action-log notes.\n  - Compare claimed source paths and Doc version labels with current source files.\n  - Report synchronized, needs-review and stale rows with expected fixes.\n  - Do not edit files, create archives, commit or push unless separately requested.\n\nuser_target:\n  <register/layer/scope to check>\n\n[/ENMAN_COMMAND]",
    "englishName": "scan register stale versions"
  },
  {
    "id": "local_sources.scan_stale",
    "group": "MVP-1",
    "label": "стейл локальные сорсы",
    "description": "local sources",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  стейл локальные сорсы\n\nenglish_name:\n  scan local source blocks\n\ncommand_family:\n  `стейл локальные сорсы` / `проверь локальные сорсы` / `local sources stale scan`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n    Then read `planning/source-cascade-sync-workflow.md`, target active files/drafts and relevant registers.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Review/audit mode by default, not edit/package mode.\n  - Use bounded target files/layer; ask scope if no active scope is clear.\n  - Check local `Sources:` blocks and file-level Source Sync sections.\n  - Check stale paths, stale version labels, stale source relationships and missing register impact.\n  - Especially check active drafts where local section Sources are authoritative.\n  - Do not edit files, create archives, commit or push unless separately requested.\n\nuser_target:\n  <files/layer/scope to check>\n\n[/ENMAN_COMMAND]",
    "englishName": "scan local source blocks"
  },
  {
    "id": "source_version.audit_full",
    "group": "MVP-1",
    "label": "полная проверка сорсов",
    "description": "full source audit",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  полная source/version проверка\n\nenglish_name:\n  full source/version audit\n\ncommand_family:\n  `полная проверка сорсов` / `полная source/version проверка` / `full source/version audit`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n    Then read `planning/source-cascade-sync-workflow.md`, relevant registers and target local Sources blocks.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Combined review/audit mode by default, not edit/package mode.\n  - Ask for scope if unclear; do not default to full repo.\n  - Run register stale-version review + local Sources stale review + active-file Doc version presence check.\n  - Check missing register impact for created/updated active files.\n  - Do not claim full root/domain/slice coverage beyond checked scope.\n  - Do not edit files, create archives, commit or push unless separately requested.\n\nuser_target:\n  <root/domain/slice/all/specific scope>\n\n[/ENMAN_COMMAND]",
    "englishName": "full source/version audit"
  },
  {
    "id": "current_state.show",
    "group": "MVP-2",
    "label": "положняк",
    "description": "current state",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  положняк\n\nenglish_name:\n  polozh\n\ncommand_family:\n  `положняк` / `текущий положняк` / `стейт` / `покажи состояние` / `current planning state`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n    Then read `planning/CURRENT-PLANNING-STATE-TEMPLATE.md` and relevant living Goal Map/register/source files if current state depends on repo evidence.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Explicit-only current-state output; do not include implicitly in Level 2 or `кц`.\n  - Output only scope-relevant areas; example Domain/Root/Slices sections are not mandatory.\n  - Use emoji plus words, not emoji alone.\n  - Distinguish current focus, next choices and do-not-claim limits.\n  - This is not Goal Map Brief and does not update files.\n  - Do not create archives, commit or push.\n\nuser_target:\n  <current scope / active workstream / state question>\n\n[/ENMAN_COMMAND]",
    "englishName": "polozh"
  },
  {
    "id": "goal_map.full",
    "group": "MVP-2",
    "label": "карта цели",
    "description": "full map",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  карта цели\n\nenglish_name:\n  full Goal Map\n\ncommand_family:\n  `карта цели` / `где мы` / `прогресс`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Full Goal Map, not compact brief.\n  - Show current goal, current state, slices, decisions and next action.\n  - Say whether the map needs sync.\n\nuser_target:\n  <goal/map target>\n\n[/ENMAN_COMMAND]",
    "englishName": "full Goal Map"
  },
  {
    "id": "repo_structure.recall",
    "group": "MVP-2",
    "label": "вспомни структуру репо",
    "description": "repo layers",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  вспомни структуру репо\n\nenglish_name:\n  repo structure brief\n\ncommand_family:\n  `вспомни структуру репо` / `структура репо` / `слои репо` / `где что лежит`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read `planning/repo-structure-memory.md` and linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Reconstruct repo root areas and documentation/code/tooling layers before planning or editing.\n  - Distinguish known structure from uncertain or unverified structure.\n  - Identify source-of-truth chain and files to read next.\n  - Do not edit files or create archive unless separately requested.\n  - Do not invent missing root files; ask for repo tree/archive if needed.\n\nuser_target:\n  <what task needs repo-structure orientation>\n\n[/ENMAN_COMMAND]",
    "englishName": "repo structure brief"
  },
  {
    "id": "output.key_points",
    "group": "MVP-2",
    "label": "кп",
    "description": "key points",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  кп\n\nenglish_name:\n  key points\n\ncommand_family:\n  `кп` / `key points`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Add `Key points first`.\n  - Key points mirror the main answer.\n  - Do not replace the detailed answer.\n  - Do not force fixed labels.\n\nuser_target:\n  <answer/context>\n\n[/ENMAN_COMMAND]",
    "englishName": "key points"
  },
  {
    "id": "output.summary",
    "group": "MVP-2",
    "label": "саммари",
    "description": "summary",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  саммари\n\nenglish_name:\n  summary\n\ncommand_family:\n  `саммари`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Add `Краткое саммари`.\n  - Use fixed summary order.\n  - This is not `План файл-обновление`.\n\nuser_target:\n  <answer/context>\n\n[/ENMAN_COMMAND]",
    "englishName": "summary"
  },
  {
    "id": "draft.show",
    "group": "MVP-2",
    "label": "давай драфт",
    "description": "draft",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  давай драфт\n\nenglish_name:\n  show draft\n\ncommand_family:\n  `драфт` / `давай драфт` / `покажи драфт`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Show/update active draft if clear.\n  - Ask target if no active draft is clear.\n  - Do not silently broaden scope.\n\nuser_target:\n  <draft target or current active draft>\n\n[/ENMAN_COMMAND]",
    "englishName": "show draft"
  },
  {
    "id": "active.update",
    "group": "MVP-2",
    "label": "обнови",
    "description": "update",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  обнови\n\nenglish_name:\n  update active item\n\ncommand_family:\n  `обнови` / `обнови драфт` / `актуализируй`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Apply latest discussion deltas.\n  - Target active draft/answer/plan.\n  - Ask target unless obvious.\n  - Say “already current” if nothing changed.\n\nuser_target:\n  <what should be updated>\n\n[/ENMAN_COMMAND]",
    "englishName": "update active item"
  },
  {
    "id": "active.clarify",
    "group": "MVP-2",
    "label": "уточни",
    "description": "clarify",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  уточни\n\nenglish_name:\n  clarify\n\ncommand_family:\n  `уточни`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Same scope.\n  - More precise wording/boundary.\n  - Do not expand or change target silently.\n\nuser_target:\n  <what should be clarified>\n\n[/ENMAN_COMMAND]",
    "englishName": "clarify"
  },
  {
    "id": "active.expand",
    "group": "MVP-2",
    "label": "расширь",
    "description": "expand",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  расширь\n\nenglish_name:\n  expand\n\ncommand_family:\n  `расширь`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Add depth/examples/edge cases.\n  - Do not silently change scope.\n  - Mention scope note if expansion could be ambiguous.\n\nuser_target:\n  <what should be expanded>\n\n[/ENMAN_COMMAND]",
    "englishName": "expand"
  },
  {
    "id": "draft.diff",
    "group": "MVP-2",
    "label": "отличия драфта",
    "description": "draft diff",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  отличия драфта\n\nenglish_name:\n  draft diff\n\ncommand_family:\n  `отличия драфта` / `draft diff`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Compare active draft with previous version.\n  - Prefer draft diff over key points for draft updates.\n  - Ask target if no active draft is clear.\n\nuser_target:\n  <draft target>\n\n[/ENMAN_COMMAND]",
    "englishName": "draft diff"
  },
  {
    "id": "output.suppress_key_points",
    "group": "MVP-2",
    "label": "без кп",
    "description": "suppress KP",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  без кп\n\nenglish_name:\n  no key points\n\ncommand_family:\n  `без кп` / `без key points`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Suppress only `Key points first`.\n  - Do not change task content.\n\nuser_target:\n  <answer/context>\n\n[/ENMAN_COMMAND]",
    "englishName": "no key points"
  },
  {
    "id": "output.suppress_summary",
    "group": "MVP-2",
    "label": "без саммари",
    "description": "suppress summary",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  без саммари\n\nenglish_name:\n  no summary\n\ncommand_family:\n  `без саммари`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Suppress only `Краткое саммари`.\n  - Do not change task content.\n\nuser_target:\n  <answer/context>\n\n[/ENMAN_COMMAND]",
    "englishName": "no summary"
  },
  {
    "id": "output.suppress_file_update_overview",
    "group": "MVP-2",
    "label": "без план файл-обновления",
    "description": "suppress FU",
    "body": "[ENMAN_COMMAND]\nRead this whole command body before answering.\nDo not ignore `key_reminders`.\n\ncommand:\n  без план файл-обновления\n\nenglish_name:\n  no file update plan\n\ncommand_family:\n  `без план файл-обновления` / `без итога`\n\nsource_of_truth:\n  Start from `planning/planning-use-case-map.md`.\n  Then read the owner / linked files for this command route if needed.\n\nroute_read_rule:\n  If you have not read this command route and its linked owner/example files in this chat, read them before answering.\n  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.\n  Do not rely only on this prompt when command behavior is uncertain.\n\nkey_reminders:\n  - Suppress only `План файл-обновление`.\n  - Do not suppress `Краткое саммари` unless separately requested.\n  - Do not change task content.\n\nuser_target:\n  <answer/context>\n\n[/ENMAN_COMMAND]",
    "englishName": "no file update plan"
  }
];

  const WIDGET_ID = 'enman-command-helper-host';
  const DRAG_THRESHOLD_PX = 6;
  const INITIAL_WIDTH_PX = 390;

  let isOpen = false;
  let panelLeft = Math.max(16, window.innerWidth - INITIAL_WIDTH_PX - 24);
  let panelTop = Math.max(16, window.innerHeight - 520);

  const existing = document.getElementById(WIDGET_ID);
  if (existing) {
    existing.remove();
  }

  const host = document.createElement('div');
  host.id = WIDGET_ID;
  document.documentElement.appendChild(host);

  const root = host.attachShadow({ mode: 'open' });

  const style = document.createElement('style');
  style.textContent = `
    :host {
      all: initial;
    }

    .enman-panel {
      position: fixed;
      left: ${panelLeft}px;
      top: ${panelTop}px;
      width: min(${INITIAL_WIDTH_PX}px, calc(100vw - 32px));
      max-height: min(70vh, 720px);
      z-index: 2147483647;
      border: 1px solid rgba(125, 125, 125, 0.35);
      border-radius: 12px;
      background: rgba(24, 24, 27, 0.96);
      color: rgb(245, 245, 245);
      font-family: ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
      font-size: 13px;
      box-shadow: 0 14px 36px rgba(0, 0, 0, 0.35);
      overflow: hidden;
      user-select: none;
    }

    .enman-header {
      display: flex;
      align-items: center;
      justify-content: space-between;
      gap: 12px;
      padding: 10px 12px;
      cursor: grab;
      background: rgba(39, 39, 42, 0.98);
      border-bottom: 1px solid rgba(125, 125, 125, 0.28);
      font-weight: 700;
      letter-spacing: 0.01em;
    }

    .enman-header:active {
      cursor: grabbing;
    }

    .enman-title {
      display: flex;
      flex-direction: column;
      gap: 2px;
      min-width: 0;
    }

    .enman-title-main {
      font-size: 13px;
      line-height: 1.1;
      white-space: nowrap;
    }

    .enman-title-sub {
      color: rgba(245, 245, 245, 0.65);
      font-size: 11px;
      font-weight: 500;
      line-height: 1.1;
      white-space: nowrap;
    }

    .enman-indicator {
      color: rgba(245, 245, 245, 0.7);
      font-size: 14px;
      line-height: 1;
    }

    .enman-body {
      max-height: calc(min(70vh, 720px) - 44px);
      overflow-y: auto;
      overscroll-behavior: contain;
      padding: 8px;
    }

    .enman-group {
      margin: 4px 0 10px;
    }

    .enman-group-title {
      color: rgba(245, 245, 245, 0.72);
      font-size: 11px;
      font-weight: 700;
      letter-spacing: 0.04em;
      text-transform: uppercase;
      padding: 6px 6px 4px;
    }

    .enman-command {
      width: 100%;
      display: grid;
      grid-template-columns: 26px minmax(0, 1fr) auto;
      align-items: center;
      gap: 8px;
      padding: 8px 7px;
      margin: 2px 0;
      border: 0;
      border-radius: 8px;
      background: transparent;
      color: inherit;
      text-align: left;
      font: inherit;
      cursor: pointer;
    }

    .enman-command:hover,
    .enman-command:focus-visible {
      outline: none;
      background: rgba(255, 255, 255, 0.09);
    }

    .enman-number {
      color: rgba(245, 245, 245, 0.45);
      font-size: 12px;
      text-align: right;
    }

    .enman-label {
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
      font-weight: 650;
    }

    .enman-description {
      color: rgba(245, 245, 245, 0.58);
      font-size: 12px;
      white-space: nowrap;
    }

    .enman-status {
      margin: 8px;
      padding: 8px;
      border-radius: 8px;
      background: rgba(255, 255, 255, 0.08);
      color: rgba(245, 245, 245, 0.78);
      line-height: 1.35;
      white-space: pre-wrap;
      user-select: text;
    }

    .enman-closed {
      width: auto;
      min-width: 116px;
    }

    .enman-closed .enman-header {
      border-bottom: 0;
    }

    .enman-closed .enman-body {
      display: none;
    }
  `;

  const panel = document.createElement('div');
  panel.className = 'enman-panel enman-closed';

  root.appendChild(style);
  root.appendChild(panel);

  function render() {
    const bodyHtml = isOpen ? renderCommandList() : '';
    panel.className = `enman-panel${isOpen ? '' : ' enman-closed'}`;
    panel.style.left = `${panelLeft}px`;
    panel.style.top = `${panelTop}px`;
    panel.innerHTML = `
      <div class="enman-header" title="Click to open/close. Drag to move.">
        <div class="enman-title">
          <div class="enman-title-main">${isOpen ? 'ENMAN commands' : 'ENMAN'}</div>
          <div class="enman-title-sub">${isOpen ? 'Click row to insert. Header toggles/drags.' : 'Click or drag header'}</div>
        </div>
        <div class="enman-indicator">${isOpen ? '⇕' : '☰'}</div>
      </div>
      <div class="enman-body">
        ${bodyHtml}
      </div>
    `;

    attachHeaderEvents();
    attachCommandEvents();
  }

  function renderCommandList() {
    const mvp1 = COMMANDS.filter((command) => command.group === 'MVP-1');
    const mvp2 = COMMANDS.filter((command) => command.group === 'MVP-2');

    return `
      ${renderGroup('MVP-1 / high-risk', mvp1, 1)}
      ${renderGroup('MVP-2 / helpers', mvp2, mvp1.length + 1)}
    `;
  }

  function getCommandDisplayLabel(command) {
    return command.englishName ? `${command.englishName} · ${command.label}` : command.label;
  }

  function renderGroup(title, commands, startNumber) {
    const rows = commands.map((command, index) => `
      <button class="enman-command" type="button" data-command-id="${escapeAttribute(command.id)}">
        <span class="enman-number">${startNumber + index}.</span>
        <span class="enman-label">${escapeHtml(getCommandDisplayLabel(command))}</span>
        <span class="enman-description">${escapeHtml(command.description)}</span>
      </button>
    `).join('');

    return `
      <section class="enman-group">
        <div class="enman-group-title">${escapeHtml(title)}</div>
        ${rows}
      </section>
    `;
  }

  function attachHeaderEvents() {
    const header = panel.querySelector('.enman-header');
    if (!header) return;

    let pointerId = null;
    let startX = 0;
    let startY = 0;
    let startLeft = 0;
    let startTop = 0;
    let dragging = false;

    header.addEventListener('pointerdown', (event) => {
      if (event.button !== 0) return;

      pointerId = event.pointerId;
      startX = event.clientX;
      startY = event.clientY;

      const rect = panel.getBoundingClientRect();
      startLeft = rect.left;
      startTop = rect.top;
      dragging = false;

      header.setPointerCapture(pointerId);
      event.preventDefault();
    });

    header.addEventListener('pointermove', (event) => {
      if (pointerId !== event.pointerId) return;

      const deltaX = event.clientX - startX;
      const deltaY = event.clientY - startY;
      const movedFarEnough = Math.hypot(deltaX, deltaY) > DRAG_THRESHOLD_PX;

      if (movedFarEnough) {
        dragging = true;
      }

      if (dragging) {
        const width = panel.offsetWidth || INITIAL_WIDTH_PX;
        const height = panel.offsetHeight || 44;
        panelLeft = clamp(startLeft + deltaX, 8, Math.max(8, window.innerWidth - width - 8));
        panelTop = clamp(startTop + deltaY, 8, Math.max(8, window.innerHeight - height - 8));
        panel.style.left = `${panelLeft}px`;
        panel.style.top = `${panelTop}px`;
        event.preventDefault();
      }
    });

    header.addEventListener('pointerup', (event) => {
      if (pointerId !== event.pointerId) return;

      try {
        header.releasePointerCapture(pointerId);
      } catch (error) {
        // Ignore release failures from browser edge cases.
      }

      if (!dragging) {
        isOpen = !isOpen;
        render();
      }

      pointerId = null;
      dragging = false;
      event.preventDefault();
    });

    header.addEventListener('pointercancel', () => {
      pointerId = null;
      dragging = false;
    });
  }

  function attachCommandEvents() {
    panel.querySelectorAll('.enman-command').forEach((button) => {
      button.addEventListener('click', () => {
        const id = button.getAttribute('data-command-id');
        const command = COMMANDS.find((item) => item.id === id);
        if (!command) return;

        const inserted = insertCommandBody(command.body);
        if (inserted) {
          showStatus(`Inserted: ${command.label}`);
        }
      });
    });
  }

  function insertCommandBody(body) {
    const composer = findComposer();
    if (!composer) {
      showStatus('Could not find ChatGPT input.\nClick into the composer and try again.');
      return false;
    }

    const currentText = getComposerText(composer).trim();
    const nextText = currentText ? `${currentText}\n\n${body}` : body;

    setComposerText(composer, nextText);
    composer.focus();
    return true;
  }

  function findComposer() {
    const selectors = [
      'textarea[data-testid="composer-textarea"]',
      'textarea[placeholder]',
      'textarea',
      '#prompt-textarea',
      '[contenteditable="true"][data-testid="composer-textarea"]',
      '[contenteditable="true"][role="textbox"]',
      '[contenteditable="true"]'
    ];

    const candidates = [];
    selectors.forEach((selector) => {
      document.querySelectorAll(selector).forEach((element) => {
        if (!candidates.includes(element) && isUsableComposerCandidate(element)) {
          candidates.push(element);
        }
      });
    });

    candidates.sort((a, b) => b.getBoundingClientRect().bottom - a.getBoundingClientRect().bottom);
    return candidates[0] || null;
  }

  function isUsableComposerCandidate(element) {
    if (!element || host.contains(element)) return false;

    const rect = element.getBoundingClientRect();
    if (rect.width <= 0 || rect.height <= 0) return false;

    const style = window.getComputedStyle(element);
    if (style.visibility === 'hidden' || style.display === 'none') return false;

    if ('disabled' in element && element.disabled) return false;
    if ('readOnly' in element && element.readOnly) return false;

    return true;
  }

  function getComposerText(element) {
    if (element instanceof HTMLTextAreaElement || element instanceof HTMLInputElement) {
      return element.value || '';
    }

    return element.innerText || element.textContent || '';
  }

  function setComposerText(element, value) {
    if (element instanceof HTMLTextAreaElement || element instanceof HTMLInputElement) {
      const valueSetter = Object.getOwnPropertyDescriptor(element.constructor.prototype, 'value')?.set;
      if (valueSetter) {
        valueSetter.call(element, value);
      } else {
        element.value = value;
      }
      dispatchInputEvents(element);
      return;
    }

    element.textContent = value;
    dispatchInputEvents(element);
  }

  function dispatchInputEvents(element) {
    try {
      element.dispatchEvent(new InputEvent('input', {
        bubbles: true,
        inputType: 'insertText',
        data: null
      }));
    } catch (error) {
      element.dispatchEvent(new Event('input', { bubbles: true }));
    }

    element.dispatchEvent(new Event('change', { bubbles: true }));
  }

  function showStatus(message) {
    const body = panel.querySelector('.enman-body');
    if (!body) return;

    const status = document.createElement('div');
    status.className = 'enman-status';
    status.textContent = message;
    body.prepend(status);

    window.setTimeout(() => {
      status.remove();
    }, 4500);
  }

  function escapeHtml(value) {
    return String(value)
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      .replace(/'/g, '&#039;');
  }

  function escapeAttribute(value) {
    return escapeHtml(value);
  }

  function clamp(value, min, max) {
    return Math.min(Math.max(value, min), max);
  }

  render();
})();
