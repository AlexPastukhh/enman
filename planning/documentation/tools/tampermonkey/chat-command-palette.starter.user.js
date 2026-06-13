// ==UserScript==
// @name         Project Chat Command Helper Starter
// @namespace    https://example.invalid/project
// @version      0.1.0
// @description  Starter command helper generated from reusable documentation-layer Tampermonkey projection rules.
// @match        https://chatgpt.com/*
// @match        https://chat.openai.com/*
// @run-at       document-idle
// @grant        none
// ==/UserScript==

/*
Reusable source:
  planning/documentation/tampermonkey-command-projection-workflow.md

Project-specific required sources after adaptation:
  planning/planning-use-case-map.md
  command owner workflows/templates named by each command route

Boundary:
  This userscript is a projection helper only.
  It is not command source of truth.
*/

(function () {
  'use strict';

  const COMMANDS = [
    {
      id: 'example.command',
      group: 'Starter',
      label: '<short command>',
      englishName: '<english display name>',
      description: '<helper description>',
      body: `[ENMAN_COMMAND]
Read this whole command body before answering.
Do not ignore \`key_reminders\`.

command:
  <short command>

english_name:
  <english display name>

command_family:
  \`<short command>\` / \`<alias>\`

source_of_truth:
  Start from \`planning/planning-use-case-map.md\`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from \`planning/planning-use-case-map.md\` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - <source/output/permission reminder>

user_target:
  <placeholder>

[/ENMAN_COMMAND]`
    }
  ];

  function displayLabel(command) {
    return command.englishName ? `${command.englishName} · ${command.label}` : command.label;
  }

  // Project implementation should add UI/insertion code here.
  window.__PROJECT_COMMAND_HELPER_STARTER__ = { COMMANDS, displayLabel };
})();
