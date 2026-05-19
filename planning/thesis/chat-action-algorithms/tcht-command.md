# Algorithm: `тчт`

If the user writes `тчт`:

1. Take the previous already sent assistant answer.
2. Save that answer into a `.txt` file.
3. Do not save the new answer that the assistant is about to produce.
4. Reply only with a short confirmation and a link to the `.txt` file.

If there is ambiguity about which previous answer is meant, use the immediately previous assistant message.
