db = db.getSiblingDB('fintech');

db.createUser({
  user: "root",
  pwd: "example",
  roles: [{ role: "readWrite", db: "fintech" }]
});
