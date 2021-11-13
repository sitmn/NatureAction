using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScript : MonoBehaviour
{
    private SkillData _skillData;

    //スキル一覧
    [SerializeField]
    private Transform[] _contentTr;
    //生成用ボタン
    [SerializeField]
    private GameObject _instantiateButtonObj;

    //キャラにセットされているスキル用のボタンイメージ
    [SerializeField]
    private Image[] _setSkillButtonImage1;
    [SerializeField]
    private Image[] _setSkillButtonImage2;

    //習得していない時の南京錠画像
    [SerializeField]
    private Sprite _padlockSprite;

    private void Awake()
    {
        _skillData = Resources.Load("SkillData") as SkillData;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    //装備スキル入替ボタンを生成し、Presenterにボタンを渡す
    public List<ButtonViewChangeSkill> SkillButtonSet()
    {
        List<ButtonViewChangeSkill> _buttonViewChangeSkillList = new List<ButtonViewChangeSkill>();

        for(int i = 0;i < ConstValue._playerAmount; i++)
        {
            int i2 = i;

            for (int j =0; j < _skillData.sheets[0].list.Count; j++)
            {
                
                int j2 = j;

                //ボタンを生成して、ボタン毎にキャラとスキルNoを設定
                GameObject _buttonObj = Instantiate(_instantiateButtonObj, _contentTr[i2]);
                _buttonViewChangeSkillList.Add(InstantiateButtonScript(_buttonObj, i2, j2));
                //スキル画像と詳細文をセット
                SetSkillDetail(_buttonObj, i2, j2);
                //ボタンの中身をセット
                //_buttonObj.GetComponent<Button>().onClick.AddListener(() => SetSkillButtonInfomation(i2, j2));
            }

            for (int j = 0; j < ConstValue._skillSetMax; j++)
            {
                InstantiateSetSkillImage(i2, GameManager.Instance._playerStatus[i2]._playerSkill[j].SkillImage, j);
            }
        }

        return _buttonViewChangeSkillList;
    }


    //ボタン毎に対象のキャラとスキルNoをセット
    private ButtonViewChangeSkill InstantiateButtonScript(GameObject _buttonObj, int _playerOperate, int _skillNo)
    {
        ButtonViewChangeSkill _buttonViewChangeSkill = _buttonObj.GetComponent<ButtonViewChangeSkill>();
        _buttonViewChangeSkill.PlayerOperate = _playerOperate;
        _buttonViewChangeSkill.SkillNo = _skillNo;

        return _buttonViewChangeSkill;
    }


    //装備しているスキルの画像セット、初期値
    private void InstantiateSetSkillImage(int _playerOperate, Sprite _skillImage, int _removeSkillNo)
    {
        if (_playerOperate == 0)
        {
            _setSkillButtonImage1[_removeSkillNo].sprite = _skillImage;
        }
        else if (_playerOperate == 1)
        {
            _setSkillButtonImage2[_removeSkillNo].sprite = _skillImage;
        }
    }

    //装備しているスキルの画像をセット
    private void SetSkillImage(int _playerOperate, int _skillNo, int _removeSkillNo)
    {
        if(_playerOperate == 0)
        {
            _setSkillButtonImage1[_removeSkillNo].sprite = _skillData.sheets[0].list[_skillNo]._skillImage;
        }else if(_playerOperate == 1)
        {
            _setSkillButtonImage2[_removeSkillNo].sprite = _skillData.sheets[0].list[_skillNo]._skillImage;
        }
    }

    //ボタンにスキルの画像と詳細をセット
    private void SetSkillDetail(GameObject _buttonObj,int _playerOperate ,int _skillNo)
    {
        //スキル画像をセット,習得している場合はスキルの画像、習得していない場合は南京錠マーク
        if(GameManager.Instance._playerStatus[_playerOperate]._playerSkillList[_skillNo] == true)
        {
            _buttonObj.GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = _skillData.sheets[0].list[_skillNo]._skillImage;
        }
        else
        {
            _buttonObj.GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = _padlockSprite;
        }
            
        //スキル詳細をセット
        _buttonObj.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "攻撃倍率：" + _skillData.sheets[0].list[_skillNo]._skillAttackMagnification.ToString()
                                                                                + "　消費MP：" + _skillData.sheets[0].list[_skillNo]._skillMP.ToString()
                                                                                + "\n" + _skillData.sheets[0].list[_skillNo]._skillDetail;
    }

    //ボタンにスキルの内容をセット
    public void SetSkillButtonInfomation(int _playerOperate, int _skillNo, int _removeSkillNo)
    {
        //習得しているスキルボタン（押すとスキルをセット）
        if(GameManager.Instance._playerStatus[_playerOperate]._playerSkillList[_skillNo] == true)
        {
            SkillSet(_playerOperate, _skillNo, _removeSkillNo);
            SetSkillImage(_playerOperate, _skillNo, _removeSkillNo);
        }
    }

    //スキルの習得
    public void SkillObtain(int _playerNo, int _skillNo)
    {
        GameManager.Instance._playerStatus[_playerNo]._playerSkillList[_skillNo] = true;
    }
    //使用スキルにセット
    //_setSkillNo: マスタデータで付番しているスキルNo
    //_removeSkillNo: プレイヤーが装備しているスキルリストの番号
    public void SkillSet(int _playerNo, int _setSkillNo, int _removeSkillNo)
    {
        Skill _skill = new Skill(_skillData.sheets[0].list[_setSkillNo]._skillName,
                                 _skillData.sheets[0].list[_setSkillNo]._skillMP,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAttackMagnification,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAnimationLength,
                                 _skillData.sheets[0].list[_setSkillNo]._skillEndTime,
                                 _skillData.sheets[0].list[_setSkillNo]._skillDurationTime,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAttackRange,
                                 _skillData.sheets[0].list[_setSkillNo]._skillSpeed,
                                 _skillData.sheets[0].list[_setSkillNo]._skillTypeName,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAnimation,
                                 _skillData.sheets[0].list[_setSkillNo]._skillColliderObj,
                                 _skillData.sheets[0].list[_setSkillNo]._skillImage,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAudioClip);

        GameManager.Instance._playerStatus[_playerNo]._playerSkill[_removeSkillNo] = _skill;
    }

    //スキル習得に必要な魔石量の導出
    public UseMagicStoneStruct UseMagicStoneCalculate(int _skillNo)
    {
        UseMagicStoneStruct _useMagicStoneStruct = new UseMagicStoneStruct(_skillData.sheets[0].list[_skillNo]._consumePurpleStone
                                                                         , _skillData.sheets[0].list[_skillNo]._consumeRedStone
                                                                         , _skillData.sheets[0].list[_skillNo]._consumeBlueStone
                                                                         , _skillData.sheets[0].list[_skillNo]._consumeGreenStone
                                                                         , _skillData.sheets[0].list[_skillNo]._consumeYellowStone);

        foreach(int _consumeStone in _useMagicStoneStruct._useMagicStone)
        {
            _useMagicStoneStruct._useSumMagicStone += _consumeStone;
        }

        return _useMagicStoneStruct;
    }
}
