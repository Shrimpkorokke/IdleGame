using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGS.Editor
{
    public class SecurityBuildPipeline : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {

        }


        public void OnPreprocessBuild(BuildReport report)
        {
            var confirm = UnityEditor.EditorPrefs.GetBool("UGS.BuildMsg", false);
            if (!confirm)
            {
                string x = "�о��ּ���! ���ȸ�尡 Ȱ��ȭ �Ǿ����� ������� ��ũ��Ʈ ���ð� UGS ���̺� ��ɵ��� �״�� �����մϴ�. ������ ������ ����Ϸ��� �ϴ°�� ���ȿɼ��� Ȱ��ȭ�ؾ��մϴ�. �ڼ��Ѱ� UGS�� ���ø޴��� �����۸�ũ���� Ȯ�����ּ���. ���� �׽�Ʈ�� ���� ���� ������ �� �޽����� �����ϼŵ� �˴ϴ�.";
                var res = UnityEditor.EditorUtility.DisplayDialog("UGS Warning", x, "�����߽��ϴ�.");
                if (res)
                {
                    UnityEditor.EditorPrefs.SetBool("UGS.BuildMsg", true);
                }

            }
        }

    }

}