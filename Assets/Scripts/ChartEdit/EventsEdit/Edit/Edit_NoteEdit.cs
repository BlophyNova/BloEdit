using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Edit_NoteEdit : MonoBehaviour
{
    public NoteEdit noteEdit;

    public TMP_Dropdown noteType;
    public Toggle noteEffect;
    public Toggle noteRipple;
    public TMP_InputField hitTime;
    public TMP_InputField endTime;
    public TMP_InputField positionX;
    public Toggle isClockwise;
    private void Start()
    {
        noteType.onValueChanged.AddListener((value) =>
        {
            noteEdit.thisNote.noteType = value switch
            {
                0 => NoteType.Tap,
                1 => NoteType.Hold,
                2 => NoteType.Drag,
                3 => NoteType.Point,
                4 => NoteType.FullFlickPink,
                5 => NoteType.FullFlickBlue,
                6 => NoteType.Flick,
                _ => throw new System.Exception("呜呜呜，，，没找到音符类型，唔姆（低下头，小声嘀咕）")
            };
            Chart.Instance.Refresh(true);
        });
        noteEffect.onValueChanged.AddListener((value) =>
        {
            /*
在C#中，如果你想要添加一个枚举值，你可以使用 | 或 |= 操作符。例如，如果你想要添加 EmailResult.STSSuccess 到 Result 中，你可以这样写：Result |= EmailResult.STSSuccess; 

如果你想要从枚举中删除一个值，你可以使用 & 和 ~ 操作符。例如，如果你想要从 colors 中删除 Blah.BLUE，你可以这样写：colors &= ~Blah.BLUE;

希望这些信息能够帮助到您！
             */
            noteEdit.thisNote.effect = value switch
            {
                true => noteEdit.thisNote.effect |= NoteEffect.CommonEffect,
                false => noteEdit.thisNote.effect &= ~NoteEffect.CommonEffect
            }; Chart.Instance.Refresh(true);
        });
        noteRipple.onValueChanged.AddListener((value) =>
        {
            noteEdit.thisNote.effect = value switch
            {
                true => noteEdit.thisNote.effect |= NoteEffect.Ripple,
                false => noteEdit.thisNote.effect &= ~NoteEffect.Ripple
            }; Chart.Instance.Refresh(true);
        });
        hitTime.onValueChanged.AddListener((value) =>
        {
            Match match = Regex.Match(value, @"(\d+):(\d+)/(\d+)");
            if (match.Success)
            {
                noteEdit.thisNote.hitTime.integer = int.Parse(match.Groups[1].Value);
                noteEdit.thisNote.hitTime.molecule = int.Parse(match.Groups[2].Value);
                noteEdit.thisNote.hitTime.denominator = int.Parse(match.Groups[3].Value);
                if (noteEdit.thisNote.noteType != NoteType.Hold)
                {
                    noteEdit.thisNote.endTime.integer = int.Parse(match.Groups[1].Value);
                    noteEdit.thisNote.endTime.molecule = int.Parse(match.Groups[2].Value);
                    noteEdit.thisNote.endTime.denominator = int.Parse(match.Groups[3].Value);
                    endTime.text = $"{noteEdit.thisNote.endTime.integer}:{noteEdit.thisNote.endTime.molecule}/{noteEdit.thisNote.endTime.denominator}";
                }
                Chart.Instance.Refresh(true);
            }
        });

        endTime.onValueChanged.AddListener((value) =>
        {
            if (noteEdit.thisNote.noteType != NoteType.Hold) return;
            Match match = Regex.Match(value, @"(\d+):(\d+)/(\d+)");
            if (match.Success)
            {
                noteEdit.thisNote.endTime.integer = int.Parse(match.Groups[1].Value);
                noteEdit.thisNote.endTime.molecule = int.Parse(match.Groups[2].Value);
                noteEdit.thisNote.endTime.denominator = int.Parse(match.Groups[3].Value);
                Chart.Instance.Refresh(true);
            }
        });
        positionX.onValueChanged.AddListener((value) =>
        {
            if (float.TryParse(value, out float result))
            {
                noteEdit.thisNote.positionX = result;
                Chart.Instance.Refresh(true);
            }
        });
        isClockwise.onValueChanged.AddListener((value) =>
        {
            noteEdit.thisNote.isClockwise = value;
            Chart.Instance.Refresh(true);
        });
    }
    public void ResetAllValue()
    {
        noteType.SetValueWithoutNotify(0);
        noteEffect.SetIsOnWithoutNotify(false);
        noteRipple.SetIsOnWithoutNotify(false);
        hitTime.SetTextWithoutNotify(string.Empty);
        endTime.SetTextWithoutNotify(string.Empty);
        positionX.SetTextWithoutNotify(string.Empty);
        isClockwise.SetIsOnWithoutNotify(false);
        noteEdit = null;
    }
    public void InitThisNoteEdit(NoteEdit noteEdit, bool changeSelectStateYesOrNo)
    {
        //this.noteEdit = noteEdit;
        if (changeSelectStateYesOrNo)
        {
            Debug.Log("InitThisNoteEdit");
            if (this.noteEdit != null)
            {
                this.noteEdit.thisNote.isSelected = false;
                this.noteEdit.image.color = new(1, 1, 1, .61f);
                Chart.Instance.RefreshPlayer();
            }
            noteEdit.thisNote.isSelected = true;
            noteEdit.image.color = Color.green;
            this.noteEdit = noteEdit;
        }
        else
        {
            Debug.Log("InitThisNoteEditWithoutChangeState");
            this.noteEdit = noteEdit;
        }
        noteType.value = noteEdit.thisNote.noteType switch
        {
            NoteType.Tap => 0,
            NoteType.Hold => 1,
            NoteType.Drag => 2,
            NoteType.Point => 3,
            NoteType.FullFlickPink => 4,
            NoteType.FullFlickBlue => 5,
            NoteType.Flick => 6,
            _ => throw new System.Exception("呜呜呜，，，没找到音符类型，唔姆（低下头，小声嘀咕）")
        };
        //noteEffect.isOn = noteEdit.thisNote.effect.HasFlag(NoteEffect.CommonEffect);
        noteEffect.SetIsOnWithoutNotify(noteEdit.thisNote.effect.HasFlag(NoteEffect.CommonEffect));
        //noteRipple.isOn = noteEdit.thisNote.effect.HasFlag(NoteEffect.Ripple);
        noteRipple.SetIsOnWithoutNotify(noteEdit.thisNote.effect.HasFlag(NoteEffect.Ripple));
        //hitTime.text = $"{noteEdit.thisNote.hitTime.integer}:{noteEdit.thisNote.hitTime.molecule}/{noteEdit.thisNote.hitTime.denominator}";
        hitTime.SetTextWithoutNotify($"{noteEdit.thisNote.hitTime.integer}:{noteEdit.thisNote.hitTime.molecule}/{noteEdit.thisNote.hitTime.denominator}");
        //endTime.text = $"{noteEdit.thisNote.endTime.integer}:{noteEdit.thisNote.endTime.molecule}/{noteEdit.thisNote.endTime.denominator}";
        endTime.SetTextWithoutNotify($"{noteEdit.thisNote.endTime.integer}:{noteEdit.thisNote.endTime.molecule}/{noteEdit.thisNote.endTime.denominator}");
        //positionX.text = $"{noteEdit.thisNote.positionX}";
        positionX.SetTextWithoutNotify($"{noteEdit.thisNote.positionX}");
        //isClockwise.isOn = noteEdit.thisNote.isClockwise;
        isClockwise.SetIsOnWithoutNotify(noteEdit.thisNote.isClockwise);
    }
}
