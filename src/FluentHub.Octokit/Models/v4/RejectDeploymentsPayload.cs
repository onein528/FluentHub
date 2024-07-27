// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of RejectDeployments
	/// </summary>
	public class RejectDeploymentsPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The affected deployments.
		/// </summary>
		public List<Deployment> Deployments { get; set; }
	}
}
