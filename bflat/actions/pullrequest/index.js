async function run() {
  const util = require("util");
  const jsExec = util.promisify(require("child_process").exec);

  console.log("Installing npm dependencies");
  const { stdout, stderr } = await jsExec("npm install @actions/core @actions/github");
  console.log("npm-install stderr:\n\n" + stderr);
  console.log("npm-install stdout:\n\n" + stdout);
  console.log("Finished installing npm dependencies");

  const core = require("@actions/core");
  const github = require("@actions/github");

  const repo_owner = github.context.payload.repository.owner.login;
  const repo_name = github.context.payload.repository.name;

  let octokit = github.getOctokit(core.getInput("auth_token", { required: true }));
  let source_branch = core.getInput("source_branch", { required: true });
  let target_branch = core.getInput("target_branch", { required: true });
  let pr_title = core.getInput("pr_title", { required: true });

  try {

    await octokit.rest.pulls.create({
      owner: repo_owner,
      repo: repo_name,
      title: pr_title,
      body: "",
      head: source_branch,
      base: target_branch
    });

    console.log("Successfully opened the GitHub PR.");
  } catch (error) {

    core.setFailed(error);

  }
}

run();
