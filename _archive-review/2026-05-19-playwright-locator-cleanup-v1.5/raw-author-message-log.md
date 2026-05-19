# Raw Author Message Log

User reported remaining failures after applying v1.4:

- agreement-exchange and employee tests fail because seed-e2e-demo-data fails with `Invalid column name 'ClientAccountId'`;
- create-individual fails because `Данные не проверены` is not found;
- my-requests and my-requests-filters fail because status locator matches hidden `<option value="InReview">На рассмотрении</option>`.
