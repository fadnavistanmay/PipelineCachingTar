workflow "New workflow" {
  on = "push"
  resolves = ["Download artifact"]
}

action "Download artifact" {
  uses = "actions/download-artifact@9cc051b66c6659c37d1418c4273dcdce10332d02"
}
