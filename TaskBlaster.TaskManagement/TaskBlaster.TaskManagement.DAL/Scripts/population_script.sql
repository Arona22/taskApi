-- Priorities initial population
INSERT INTO "public"."Priorities" ("Name", "Description")
VALUES ('Critical', 'Tasks that must be addressed immediately');

INSERT INTO "public"."Priorities" ("Name", "Description")
VALUES ('High', 'Important tasks that should be prioritized after critical ones');

INSERT INTO "public"."Priorities" ("Name", "Description")
VALUES ('Medium', 'Tasks that are important but not urgent');

INSERT INTO "public"."Priorities" ("Name", "Description")
VALUES ('Low', 'Tasks of lesser importance that can be attended to last');

-- Statuses initial population
INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('Backlog', 'Task is not started yet');

INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('In Progress', 'Task is currently being worked on');

INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('Completed', 'Task has been finished');

INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('On Hold', 'Task is temporarily paused');

INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('In Review', 'Task is being reviewed before completion');
